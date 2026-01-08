using backend.DTOs.Borrow;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services.Implements
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepo;
        private readonly IEquipmentRepository _equipmentRepo;

        public BorrowService(IBorrowRepository borrowRepo, IEquipmentRepository equipmentRepo)
        {
            _borrowRepo = borrowRepo;
            _equipmentRepo = equipmentRepo;
        }

        public async Task<IEnumerable<BorrowVoucher>> GetHistory()
        {
            return await _borrowRepo.GetAllAsync();
        }

        public async Task<BorrowVoucher> CreateRequest(BorrowRequestDTO dto)
        {
            var voucher = new BorrowVoucher
            {
                BorrowerId = dto.BorrowerId,
                Purpose = dto.Purpose,
                Status = BorrowStatus.Pending, // Mặc định là Chờ duyệt
                BorrowDetails = new List<BorrowDetail>()
            };

            foreach (var item in dto.Items)
            {
                var equipment = await _equipmentRepo.GetEquipmentByIdAsync(item.EquipmentId);
                if (equipment == null)
                    throw new BadRequestException($"The device {item.EquipmentId} does not exist.");

                if (equipment.Quantity < item.Quantity)
                    throw new BadRequestException($"The device {equipment.EquipmentName} has only {equipment.Quantity} left, Not enough to borrow {item.Quantity}.");

                voucher.BorrowDetails.Add(new BorrowDetail
                {
                    BorrowId = voucher.BorrowId,
                    EquipmentId = item.EquipmentId,
                    EquipmentName = equipment.EquipmentName,
                    Quantity = item.Quantity
                });
            }

            await _borrowRepo.AddAsync(voucher);
            return voucher;
        }

        public async Task<BorrowVoucher> ApproveRequest(string voucherId, ApproveBorrowDTO dto)
        {
            var voucher = await _borrowRepo.GetByIdAsync(voucherId);
            if (voucher == null) throw new Exception("Borrow Voucher is not found.");

            if (voucher.Status != BorrowStatus.Pending)
                throw new BadRequestException("This Borrow Voucher is already processed");

            voucher.StatusUpdatedBy = dto.ApproverId;
            voucher.StatusUpdatedAt = DateTime.UtcNow;

            if (!dto.IsApproved)
            {
                voucher.Status = BorrowStatus.Rejected;
            }
            else
            {
                foreach (var detail in voucher.BorrowDetails)
                {
                    var equipment = await _equipmentRepo.GetEquipmentByIdAsync(detail.EquipmentId);

                    if (equipment == null || equipment.Quantity < detail.Quantity)
                        throw new BadRequestException($"Insufficient stock for the device: {detail.EquipmentName}");

                    equipment.Quantity -= detail.Quantity;

                    await _equipmentRepo.UpdateEquipmentAsync(equipment);
                }

                voucher.Status = BorrowStatus.Approved;
            }

            voucher.StatusUpdatedAt = DateTime.UtcNow;
            await _borrowRepo.UpdateAsync(voucher);

            return voucher;
        }

        public async Task<BorrowVoucher> ReturnDevice(string voucherId)
        {
            var voucher = await _borrowRepo.GetByIdAsync(voucherId);

            if (voucher == null) throw new NotFoundException("No voucher found.");
            if (voucher.Status != BorrowStatus.Approved)
                throw new Exception("The voucher has not been approved or is already completed and cannot be returned.");

            foreach (var detail in voucher.BorrowDetails)
            {
                var equipment = await _equipmentRepo.GetEquipmentByIdAsync(detail.EquipmentId);
                if (equipment != null)
                {
                    equipment.Quantity += detail.Quantity;
                    await _equipmentRepo.UpdateEquipmentAsync(equipment);
                }
            }

            voucher.Status = BorrowStatus.Returned;
            voucher.StatusUpdatedAt = DateTime.UtcNow;

            await _borrowRepo.UpdateAsync(voucher);
            return voucher;
        }
    }
}