using AutoMapper;
using backend.Data;
using backend.DTOs.Maintenance.Request;
using backend.DTOs.Maintenance.Response;
using backend.Exceptions;
using backend.Models.Maintenance;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRequestRepository _requestRepo;
        private readonly IMaintenanceVoucherRepository _voucherRepo;
        private readonly DataApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MaintenanceService(
            IMaintenanceRequestRepository requestRepo,
            IMaintenanceVoucherRepository voucherRepo,
            DataApplicationDbContext context,
            IMapper mapper)
        {
            _requestRepo = requestRepo;
            _voucherRepo = voucherRepo;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageVO<MaintenanceRequestResponse>> GetRequests(EntityFilter<MaintenanceRequest> filter, EntitySort<MaintenanceRequest> sort, int page, int size)
        {
            var paged = await _requestRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<MaintenanceRequestResponse>>(paged.Content);
            return new PageVO<MaintenanceRequestResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }
        public async Task<MaintenanceRequestResponse> CreateRequest(CreateMaintenanceRequestRequest request)
        {
            var entity = _mapper.Map<MaintenanceRequest>(request);
            entity.Status = Enums.VoucherStatus.Pending;

            if (entity.Details == null || !entity.Details.Any())
            {
                throw new Exception("The list of equipment to be maintained must not be left blank.");
            }

            await _requestRepo.AddAsync(entity);
            return _mapper.Map<MaintenanceRequestResponse>(entity);
        }
        public async Task<MaintenanceRequestResponse> ApproveRequest(string requestId, UpdateMaintenanceRequestStatusRequest request)
        {
            var entity = await _requestRepo.GetByIdAsync(requestId);
            if (entity == null) throw new Exception("No request found.");
            if (entity.Status != Enums.VoucherStatus.Pending)
            {
                throw new Exception("Only requests with 'Pending' status can be approved.");
            }

            entity.Status = request.Status;
            entity.ApprovedBy = request.ApprovedBy;
            entity.ApprovedAt = DateTime.Now;

            await _requestRepo.UpdateAsync(entity);
            return _mapper.Map<MaintenanceRequestResponse>(entity);
        }
        public async Task<PageVO<MaintenanceVoucherResponse>> GetVouchers(EntityFilter<MaintenanceVoucher> filter, EntitySort<MaintenanceVoucher> sort, int page, int size)
        {
            var paged = await _voucherRepo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<MaintenanceVoucherResponse>>(paged.Content);
            return new PageVO<MaintenanceVoucherResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }
        public async Task<MaintenanceVoucherResponse> CreateVoucher(CreateMaintenanceVoucherRequest request)
        {
            var transferRequest = await _requestRepo.GetByIdAsync(request.RequestId);
            if (transferRequest == null)
            {
                throw new Exception("No maintenance request found for the provided RequestId.");
            }
            if (transferRequest.Status != Enums.VoucherStatus.Approved)
            {
                throw new Exception("Only approved maintenance requests can have vouchers created.");
            }
            if (await _voucherRepo.ExistsByRequestIdAsync(request.RequestId))
            {
                throw new Exception("A maintenance voucher has already been created for this request.");
            }
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var voucher = new MaintenanceVoucher
                {
                    RequestId = request.RequestId,
                    CreatedBy = request.CreatedBy,
                    Details = new List<MaintenanceVoucherDetail>()
                };
                foreach (var detail in transferRequest.Details)
                {
                    var voucherDetail = new MaintenanceVoucherDetail
                    {
                        EquipmentId = detail.EquipmentId,
                        Note = detail.Note
                    };
                    voucher.Details.Add(voucherDetail);
                    var equipment = await _context.Equipments.FindAsync(detail.EquipmentId);
                    if (equipment != null)
                    {
                        equipment.Status = Enums.EquipmentStatus.UnderMaintenance;
                        //_context.Equipments.Update(equipment);
                    }
                    else
                    {
                        throw new NotFoundException($"No device with ID found: {detail.EquipmentId}");
                    }
                }
                await _voucherRepo.AddAsync(voucher);
                await transaction.CommitAsync();

                var completedVoucher = await _voucherRepo.GetByIdAsync(voucher.VoucherId);
                return _mapper.Map<MaintenanceVoucherResponse>(completedVoucher);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
