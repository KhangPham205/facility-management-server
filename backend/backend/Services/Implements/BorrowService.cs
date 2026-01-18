using AutoMapper;
using backend.Data;
using backend.DTOs.Borrow.Request;
using backend.DTOs.Borrow.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Borrow;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Utils;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _repo;
        private readonly DataApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public BorrowService(IBorrowRepository repo, DataApplicationDbContext context, IMapper mapper, JwtUtils jwtUtils)
        {
            _repo = repo;
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        public async Task<PageVO<BorrowVoucherResponse>> GetAll(EntityFilter<BorrowVoucher> filter, EntitySort<BorrowVoucher> sort, int page, int size)
        {
            var paged = await _repo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<BorrowVoucherResponse>>(paged.Content);
            return new PageVO<BorrowVoucherResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }

        public async Task<BorrowVoucherResponse> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("No borrow voucher found.");
            return _mapper.Map<BorrowVoucherResponse>(entity);
        }

        public async Task<BorrowVoucherResponse> Create(CreateBorrowRequest request)
        {
            var currentUserId = _jwtUtils.GetCurrentUserId();

            if (string.IsNullOrEmpty(request.BorrowerId))
            {
                request.BorrowerId = currentUserId;
            }

            if (request.Details == null || !request.Details.Any())
                throw new BadRequestException("The list of equipments cannot be left blank.");

            foreach (var item in request.Details)
            {
                var equipment = await _context.Equipments.FindAsync(item.EquipmentId);
                if (equipment == null)
                    throw new NotFoundException($"Equipment not found: {item.EquipmentId}");

                if (equipment.Status != EquipmentStatus.Available)
                    throw new BadRequestException($"The equipment {equipment.EquipmentName} is not available (Status: {equipment.Status}).");
            }

            var voucher = new BorrowVoucher
            {
                CreatedBy = currentUserId,
                BorrowerId = request.BorrowerId,
                Note = request.Note,
                ReturnDate = request.ReturnDate, // Ngày dự kiến
                Status = BorrowStatus.Pending,
                Details = request.Details.Select(d => new BorrowVoucherDetail
                {
                    EquipmentId = d.EquipmentId,
                    Note = d.Note
                }).ToList()
            };

            await _repo.AddAsync(voucher);
            return await GetById(voucher.BorrowId);
        }

        public async Task<BorrowVoucherResponse> Approve(string id, ApproveBorrowRequest request)
        {
            var voucher = await _repo.GetByIdAsync(id);
            if (voucher == null) throw new NotFoundException("No borrow voucher found.");

            if (voucher.Status != BorrowStatus.Pending)
                throw new BadRequestException("This voucher has been processed previously.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                voucher.Status = request.Status;
                voucher.ApprovedBy = _jwtUtils.GetCurrentUserId();
                voucher.ApprovedAt = DateTime.Now;

                if (request.Status == BorrowStatus.Approved)
                {
                    // Nếu duyệt -> Đổi trạng thái thiết bị sang Borrowed
                    voucher.Status = BorrowStatus.Borrowing;

                    foreach (var detail in voucher.Details)
                    {
                        var equipment = await _context.Equipments.FindAsync(detail.EquipmentId);
                        if (equipment != null)
                        {
                            equipment.Status = EquipmentStatus.Borrowed;
                        }
                    }
                }
                else if (request.Status == BorrowStatus.Rejected)
                {
                    // Nếu từ chối -> Không làm gì với thiết bị
                }

                await _repo.UpdateAsync(voucher);
                await transaction.CommitAsync();

                return _mapper.Map<BorrowVoucherResponse>(voucher);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BorrowVoucherResponse> Return(string id)
        {
            var voucher = await _repo.GetByIdAsync(id);
            if (voucher == null) throw new NotFoundException("No borrow voucher found.");

            if (voucher.Status != BorrowStatus.Borrowing)
                throw new BadRequestException("This voucher has either not been approved or has already been paid.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                voucher.Status = BorrowStatus.Returned;
                voucher.ReturnDate = DateTime.Now; // Ngày trả thực tế

                foreach (var detail in voucher.Details)
                {
                    var equipment = await _context.Equipments.FindAsync(detail.EquipmentId);
                    if (equipment != null)
                    {
                        equipment.Status = EquipmentStatus.Available;
                    }
                }

                await _repo.UpdateAsync(voucher);
                await transaction.CommitAsync();

                return _mapper.Map<BorrowVoucherResponse>(voucher);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}