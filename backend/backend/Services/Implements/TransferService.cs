using AutoMapper;
using backend.Data;
using backend.DTOs.Transfer.Request;
using backend.DTOs.Transfer.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Transfer;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRequestRepository _requestRepo;
        private readonly ITransferVoucherRepository _voucherRepo;
        private readonly DataApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransferService(
            ITransferRequestRepository requestRepo,
            ITransferVoucherRepository voucherRepo,
            DataApplicationDbContext context,
            IMapper mapper)
        {
            _requestRepo = requestRepo;
            _voucherRepo = voucherRepo;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageVO<TransferRequestResponse>> GetRequests(EntityFilter<TransferRequest> filter, EntitySort<TransferRequest> sort, int page, int size)
        {
            var paged = await _requestRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<TransferRequestResponse>>(paged.Content);
            return new PageVO<TransferRequestResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }

        public async Task<TransferRequestResponse> CreateRequest(CreateTransferRequestRequest request)
        {
            var entity = _mapper.Map<TransferRequest>(request);
            entity.Status = VoucherStatus.Pending;

            if (entity.Details == null || !entity.Details.Any())
            {
                throw new BadRequestException("The list of equipment to be transferred must not be left blank.");
            }

            await _requestRepo.AddAsync(entity);
            return _mapper.Map<TransferRequestResponse>(entity);
        }

        public async Task<TransferRequestResponse> ApproveRequest(string requestId, ApproveTransferRequest request)
        {
            var entity = await _requestRepo.GetByIdAsync(requestId);
            if (entity == null) throw new NotFoundException("No request found.");

            if (entity.Status != VoucherStatus.Pending)
                throw new BadRequestException("The request was processed previously.");

            entity.Status = request.Status;
            entity.ApprovedBy = request.ApprovedBy;
            entity.ApprovedAt = DateTime.Now;

            await _requestRepo.UpdateAsync(entity);
            return _mapper.Map<TransferRequestResponse>(entity);
        }


        public async Task<PageVO<TransferVoucherResponse>> GetVouchers(EntityFilter<TransferVoucher> filter, EntitySort<TransferVoucher> sort, int page, int size)
        {
            var paged = await _voucherRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<TransferVoucherResponse>>(paged.Content);

            await PopulateLocationNames(dtos);

            return new PageVO<TransferVoucherResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }

        public async Task<TransferVoucherResponse> CreateVoucher(CreateTransferVoucherRequest request)
        {
            var transferRequest = await _requestRepo.GetByIdAsync(request.RequestId);
            if (transferRequest == null)
                throw new NotFoundException("No transfer requests were found.");

            if (transferRequest.Status != VoucherStatus.Approved)
                throw new BadRequestException("The request must be approved before creating the transfer slip.");

            if (await _voucherRepo.ExistsByRequestIdAsync(request.RequestId))
                throw new BadRequestException("A transfer voucher has already been created for this request.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var voucher = new TransferVoucher
                {
                    RequestId = request.RequestId,
                    CreatedBy = request.CreatedBy,
                    SourceLocationId = transferRequest.SourceLocationId,
                    SourceLocationType = transferRequest.SourceLocationType,

                    DestinationLocationId = transferRequest.DestinationLocationId,
                    DestinationLocationType = transferRequest.DestinationLocationType,

                    Details = new List<TransferVoucherDetail>()
                };

                foreach (var reqDetail in transferRequest.Details)
                {
                    var voucherDetail = new TransferVoucherDetail
                    {
                        EquipmentId = reqDetail.EquipmentId,
                        Note = reqDetail.Note
                    };
                    voucher.Details.Add(voucherDetail);

                    var equipment = await _context.Equipments.FindAsync(reqDetail.EquipmentId);
                    if (equipment != null)
                    {
                        equipment.LocationId = transferRequest.DestinationLocationId;
                        equipment.LocationType = transferRequest.DestinationLocationType;

                        // Update trạng thái thiết bị
                        equipment.Status = EquipmentStatus.Available;
                    }
                    else
                    {
                        throw new NotFoundException($"No device with ID found: {reqDetail.EquipmentId}");
                    }
                }

                await _voucherRepo.AddAsync(voucher);
                await transaction.CommitAsync();

                var completeVoucher = await _voucherRepo.GetByIdAsync(voucher.TransferId);
                var response = _mapper.Map<TransferVoucherResponse>(completeVoucher);

                await PopulateLocationNames(new List<TransferVoucherResponse> { response });

                return response;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        // HELPER METHOD: POPULATE LOCATION NAMES
        private async Task PopulateLocationNames(List<TransferVoucherResponse> dtos)
        {
            if (dtos == null || !dtos.Any()) return;

            var roomIds = dtos.Where(x => x.SourceLocationType == LocationType.Room).Select(x => x.SourceLocationId)
                .Concat(dtos.Where(x => x.DestinationLocationType == LocationType.Room).Select(x => x.DestinationLocationId))
                .Distinct().ToList();

            var floorIds = dtos.Where(x => x.SourceLocationType == LocationType.Floor).Select(x => x.SourceLocationId)
                .Concat(dtos.Where(x => x.DestinationLocationType == LocationType.Floor).Select(x => x.DestinationLocationId))
                .Distinct().ToList();

            var buildingIds = dtos.Where(x => x.SourceLocationType == LocationType.Building).Select(x => x.SourceLocationId)
                .Concat(dtos.Where(x => x.DestinationLocationType == LocationType.Building).Select(x => x.DestinationLocationId))
                .Distinct().ToList();

            var rooms = new Dictionary<string, string>();
            if (roomIds.Any())
            {
                rooms = await _context.Rooms
                    .Where(r => roomIds.Contains(r.RoomId))
                    .ToDictionaryAsync(r => r.RoomId, r => r.RoomName);
            }

            var floors = new Dictionary<string, string>();
            if (floorIds.Any())
            {
                floors = await _context.Floors
                    .Where(f => floorIds.Contains(f.FloorId))
                    .ToDictionaryAsync(f => f.FloorId, f => f.FloorName);
            }

            var buildings = new Dictionary<string, string>();
            if (buildingIds.Any())
            {
                buildings = await _context.Buildings
                    .Where(b => buildingIds.Contains(b.BuildingId))
                    .ToDictionaryAsync(b => b.BuildingId, b => b.BuildingName);
            }

            foreach (var dto in dtos)
            {
                dto.SourceLocationName = GetLocationName(dto.SourceLocationType, dto.SourceLocationId, rooms, floors, buildings);
                dto.DestinationLocationName = GetLocationName(dto.DestinationLocationType, dto.DestinationLocationId, rooms, floors, buildings);
            }
        }

        private string GetLocationName(
            LocationType type,
            string id,
            Dictionary<string, string> rooms,
            Dictionary<string, string> floors,
            Dictionary<string, string> buildings)
        {
            return type switch
            {
                LocationType.Room => rooms.ContainsKey(id) ? rooms[id] : "Unknown Room",
                LocationType.Floor => floors.ContainsKey(id) ? floors[id] : "Unknown Floor",
                LocationType.Building => buildings.ContainsKey(id) ? buildings[id] : "Unknown Building",
                _ => "Unknown Location"
            };
        }
    }
}
