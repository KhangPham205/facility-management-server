using AutoMapper;
using backend.Data;
using backend.DTOs.Transfer.Request;
using backend.DTOs.Transfer.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Transfer;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Utils;
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
        private readonly JwtUtils _jwtUtils; // 1. Inject JwtUtils

        public TransferService(
            ITransferRequestRepository requestRepo,
            ITransferVoucherRepository voucherRepo,
            DataApplicationDbContext context,
            IMapper mapper,
            JwtUtils jwtUtils)
        {
            _requestRepo = requestRepo;
            _voucherRepo = voucherRepo;
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        // =========================================================================
        // TRANSFER REQUEST (YÊU CẦU ĐIỀU CHUYỂN)
        // =========================================================================

        public async Task<PageVO<TransferRequestResponse>> GetRequests(EntityFilter<TransferRequest> filter, EntitySort<TransferRequest> sort, int page, int size)
        {
            var paged = await _requestRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<TransferRequestResponse>>(paged.Content);

            await PopulateLocationNames(dtos);

            return new PageVO<TransferRequestResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }

        public async Task<TransferRequestResponse> GetRequestById(string id)
        {
            var entity = await _requestRepo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("Transfer request not found.");

            var dto = _mapper.Map<TransferRequestResponse>(entity);
            await PopulateLocationNames(new List<TransferRequestResponse> { dto });

            return dto;
        }

        public async Task<TransferRequestResponse> CreateRequest(CreateTransferRequestRequest request)
        {
            var entity = _mapper.Map<TransferRequest>(request);

            entity.CreatedBy = _jwtUtils.GetCurrentUserId();
            entity.CreatedAt = DateTime.Now;
            entity.Status = VoucherStatus.Pending;

            if (entity.Details == null || !entity.Details.Any())
            {
                throw new BadRequestException("The transfer equipment list cannot be empty.");
            }

            await _requestRepo.AddAsync(entity);

            return await GetRequestById(entity.RequestId);
        }

        public async Task<TransferRequestResponse> ApproveRequest(string requestId, ApproveTransferRequest request)
        {
            var entity = await _requestRepo.GetByIdAsync(requestId);
            if (entity == null) throw new NotFoundException("Request not found.");

            if (entity.Status != VoucherStatus.Pending)
                throw new BadRequestException("This request has already been processed.");

            entity.Status = request.Status;
            entity.ApprovedBy = _jwtUtils.GetCurrentUserId();
            entity.ApprovedAt = DateTime.Now;

            await _requestRepo.UpdateAsync(entity);
            return _mapper.Map<TransferRequestResponse>(entity);
        }

        // =========================================================================
        // TRANSFER VOUCHER (PHIẾU ĐIỀU CHUYỂN / THỰC THI)
        // =========================================================================

        public async Task<PageVO<TransferVoucherResponse>> GetVouchers(EntityFilter<TransferVoucher> filter, EntitySort<TransferVoucher> sort, int page, int size)
        {
            var paged = await _voucherRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<TransferVoucherResponse>>(paged.Content);

            await PopulateLocationNames(dtos);

            return new PageVO<TransferVoucherResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }

        public async Task<TransferVoucherResponse> GetVoucherById(string id)
        {
            var entity = await _voucherRepo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("Transfer voucher not found.");

            var dto = _mapper.Map<TransferVoucherResponse>(entity);
            await PopulateLocationNames(new List<TransferVoucherResponse> { dto });

            return dto;
        }

        public async Task<TransferVoucherResponse> CreateVoucher(CreateTransferVoucherRequest request)
        {
            var transferRequest = await _requestRepo.GetByIdAsync(request.RequestId);
            if (transferRequest == null)
                throw new NotFoundException("Original transfer request not found.");

            if (transferRequest.Status != VoucherStatus.Approved)
                throw new BadRequestException("The request must be approved before creating a transfer voucher.");

            if (await _voucherRepo.ExistsByRequestIdAsync(request.RequestId))
                throw new BadRequestException("A transfer voucher has already been created for this request.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var voucher = new TransferVoucher
                {
                    RequestId = request.RequestId,
                    CreatedBy = _jwtUtils.GetCurrentUserId(),
                    CreatedAt = DateTime.Now,

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

                        equipment.Status = EquipmentStatus.Available;
                    }
                    else
                    {
                        throw new NotFoundException($"Equipment with ID {reqDetail.EquipmentId} not found.");
                    }
                }

                await _voucherRepo.AddAsync(voucher);
                await transaction.CommitAsync();

                return await GetVoucherById(voucher.TransferId);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // HELPER METHODS: POPULATE LOCATION NAMES
        private async Task PopulateLocationNames(List<TransferVoucherResponse> dtos)
        {
            if (dtos == null || !dtos.Any()) return;

            var (rooms, floors, buildings) = await FetchLocationMaps(
                dtos.Select(x => (x.SourceLocationId, x.SourceLocationType))
                    .Concat(dtos.Select(x => (x.DestinationLocationId, x.DestinationLocationType)))
            );

            foreach (var dto in dtos)
            {
                dto.SourceLocationName = GetLocationName(dto.SourceLocationType, dto.SourceLocationId, rooms, floors, buildings);
                dto.DestinationLocationName = GetLocationName(dto.DestinationLocationType, dto.DestinationLocationId, rooms, floors, buildings);
            }
        }

        private async Task PopulateLocationNames(List<TransferRequestResponse> dtos)
        {
            if (dtos == null || !dtos.Any()) return;

            var (rooms, floors, buildings) = await FetchLocationMaps(
                dtos.Select(x => (x.SourceLocationId, x.SourceLocationType))
                    .Concat(dtos.Select(x => (x.DestinationLocationId, x.DestinationLocationType)))
            );

            foreach (var dto in dtos)
            {
                dto.SourceLocationName = GetLocationName(dto.SourceLocationType, dto.SourceLocationId, rooms, floors, buildings);
                dto.DestinationLocationName = GetLocationName(dto.DestinationLocationType, dto.DestinationLocationId, rooms, floors, buildings);
            }
        }

        private async Task<(Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>)>
            FetchLocationMaps(IEnumerable<(string Id, LocationType Type)> locations)
        {
            var locList = locations.Distinct().ToList();

            var roomIds = locList.Where(x => x.Type == LocationType.Room).Select(x => x.Id).Distinct().ToList();
            var floorIds = locList.Where(x => x.Type == LocationType.Floor).Select(x => x.Id).Distinct().ToList();
            var buildingIds = locList.Where(x => x.Type == LocationType.Building).Select(x => x.Id).Distinct().ToList();

            var rooms = roomIds.Any()
                ? await _context.Rooms.Where(r => roomIds.Contains(r.RoomId)).ToDictionaryAsync(r => r.RoomId, r => r.RoomName)
                : new Dictionary<string, string>();

            var floors = floorIds.Any()
                ? await _context.Floors.Where(f => floorIds.Contains(f.FloorId)).ToDictionaryAsync(f => f.FloorId, f => f.FloorName)
                : new Dictionary<string, string>();

            var buildings = buildingIds.Any()
                ? await _context.Buildings.Where(b => buildingIds.Contains(b.BuildingId)).ToDictionaryAsync(b => b.BuildingId, b => b.BuildingName)
                : new Dictionary<string, string>();

            return (rooms, floors, buildings);
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