using AutoMapper;
using backend.Data;
using backend.DTOs.Repair.Request;
using backend.DTOs.Repair.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Repair;
using backend.Models.Transfer;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class RepairService : IRepairService
    {
        private readonly IRepairRequestRepository _requestRepo;
        private readonly IRepairVoucherRepository _voucherRepo;
        private readonly DataApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RepairService(
            IRepairRequestRepository requestRepo,
            IRepairVoucherRepository voucherRepo,
            DataApplicationDbContext context,
            IMapper mapper)
        {
            _requestRepo = requestRepo;
            _voucherRepo = voucherRepo;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageVO<RepairRequestResponse>> GetRequests(EntityFilter<RepairRequest> filter, EntitySort<RepairRequest> sort, int page, int size)
        {
            var paged = await _requestRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = paged.Content.Select(request => new RepairRequestResponse
            {
                RequestId = request.RequestId,
                CreatedByName = request.Creator != null ? request.Creator.Fullname : string.Empty,
                CreatedAt = request.CreatedAt,
                Note = request.Note ?? string.Empty,
                Status = request.Status,
                Details = request.Details.Select(detail => new RepairRequestDetailResponse
                {
                    EquipmentId = detail.EquipmentId,
                    EquipmentName = detail.Equipment != null ? detail.Equipment.EquipmentName : string.Empty,
                    Note = detail.Note
                }).ToList()
            }).ToList();
            return new PageVO<RepairRequestResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }
        public async Task<RepairRequestResponse> GetRequestById(string requestId)
        {
            var entity = await _requestRepo.GetByIdAsync(requestId);
            if (entity == null) throw new Exception("No request found.");
            return _mapper.Map<RepairRequestResponse>(entity);
        }
        public async Task CreateRequest(CreateRepairRequestRequest request)
        {
            var entity = _mapper.Map<RepairRequest>(request);
            entity.Status = VoucherStatus.Pending;

            if (entity.Details == null || !entity.Details.Any())
            {
                throw new Exception("The list of equipment to be repaired must not be left blank.");
            }

            await _requestRepo.AddAsync(entity);
        }
        public async Task ApproveRequest(string requestId, UpdateRepairRequestStatusRequest request)
        {
            var entity = await _requestRepo.GetByIdAsync(requestId);
            if (entity == null) throw new Exception("No request found.");
            if (entity.Status != VoucherStatus.Pending)
            {
                throw new Exception("Only requests with 'Pending' status can be approved.");
            }

            entity.Status = request.Status;
            entity.ApprovedBy = request.ApprovedBy;
            entity.ApprovedAt = DateTime.Now;

            await _requestRepo.UpdateAsync(entity);
        }
        public async Task<PageVO<RepairVoucherResponse>> GetVouchers(EntityFilter<RepairVoucher> filter, EntitySort<RepairVoucher> sort, int page, int size)
        {
            var paged = await _voucherRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = paged.Content.Select(voucher => new RepairVoucherResponse
            {
                VoucherId = voucher.RepairId,
                InvoiceNumber = voucher.Invoice != null ? voucher.Invoice.InvoiceNumber : string.Empty,
                TotalAmount = voucher.Invoice != null ? voucher.Invoice.TotalAmount : 0,
                Status = voucher.Status,
                ProviderName = voucher.Provider != null ? voucher.Provider.UnitName : string.Empty,
                Details = voucher.Details.Select(detail => new RepairRequestDetailResponse
                {
                    EquipmentId = detail.EquipmentId,
                    EquipmentName = detail.Equipment != null ? detail.Equipment.EquipmentName : string.Empty,
                    Note = detail.Note
                }).ToList()
            }).ToList();
            return new PageVO<RepairVoucherResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }
        public async Task<RepairVoucherResponse> GetVoucherById(string voucherId)
        {
            var entity = await _voucherRepo.GetByIdAsync(voucherId);
            if (entity == null) throw new Exception("No voucher found.");
            return new RepairVoucherResponse
            {
                VoucherId = entity.RepairId,
                InvoiceNumber = entity.Invoice != null ? entity.Invoice.InvoiceNumber : string.Empty,
                TotalAmount = entity.Invoice != null ? entity.Invoice.TotalAmount : 0,
                Status = entity.Status,
                ProviderName = entity.Provider != null ? entity.Provider.UnitName : string.Empty,
                Details = entity.Details.Select(detail => new RepairRequestDetailResponse
                {
                    EquipmentId = detail.EquipmentId,
                    EquipmentName = detail.Equipment != null ? detail.Equipment.EquipmentName : string.Empty,
                    Note = detail.Note
                }).ToList()
            };
        }
        public async Task CreateVoucher(CreateRepairVoucherRequest request)
        {
            var repairRequest = await _requestRepo.GetByIdAsync(request.RequestId);
            if (repairRequest == null)
            {
                throw new Exception("No repair request found for the given RequestId.");
            }
            if (repairRequest.Status != VoucherStatus.Approved)
            {
                throw new Exception("Only approved repair requests can have vouchers created.");
            }
            if (await _voucherRepo.ExistsByRequestIdAsync(request.RequestId))
            {
                throw new Exception("A repair voucher has already been created for this request.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var voucher = new RepairVoucher
                {
                    RequestId = request.RequestId,
                    CreatedBy = request.CreatedBy,
                    Details = new List<RepairVoucherDetail>()
                };
                foreach (var detail in repairRequest.Details)
                {
                    var voucherDetail = new RepairVoucherDetail
                    {
                        EquipmentId = detail.EquipmentId,
                        Note = detail.Note
                    };
                    voucher.Details.Add(voucherDetail);

                    var equipment = await _context.Equipments.FindAsync(detail.EquipmentId);
                    if (equipment != null)
                    {
                        equipment.Status = EquipmentStatus.Broken;
                        //_context.Equipments.Update(equipment);
                    }
                    else
                    {
                        throw new NotFoundException($"No device with ID found: {detail.EquipmentId}");
                    }
                }
                await _voucherRepo.AddAsync(voucher);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateVoucher(string voucherId, UpdateRepairVoucherStatusRequest request)
        {
            var entity = await _voucherRepo.GetByIdAsync(voucherId);
            if (entity == null) throw new Exception("No voucher found.");
            entity.Status = request.Status;
            await _voucherRepo.UpdateAsync(entity);
        }
    }
}
