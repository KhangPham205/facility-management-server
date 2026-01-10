using backend.DTOs.Equipment;
using backend.DTOs.Transfer;
using backend.Enums;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;

namespace backend.Services.Implements
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepo;
        private readonly IEquipmentRepository _equipmentRepo;

        public TransferService(ITransferRepository transferRepo, IEquipmentRepository equipmentRepo)
        {
            _transferRepo = transferRepo;
            _equipmentRepo = equipmentRepo;
        }

        public async Task<PageVO<TransferResponseDTO>> GetAll(int page, int size)
        {
            var pagedResult = await _transferRepo.GetAllPagedAsync(page, size);

            var dtoList = pagedResult.Content.Select(t => MapToDto(t)).ToList();

            return new PageVO<TransferResponseDTO>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<TransferResponseDTO> CreateRequest(CreateTransferDTO dto)
        {
            var equipment = await _equipmentRepo.GetEquipmentByIdAsync(dto.EquipmentId);
            if (equipment == null)
                throw new Exception($"The quipment {dto.EquipmentId} does not exist");

            if (equipment.RoomId != dto.SourceLocation)
                throw new Exception($"The equipment is in the room {equipment.RoomId}, not in the room {dto.SourceLocation}.");

            var voucher = new TransferVoucher
            {
                TransferId = Guid.NewGuid().ToString(),
                CreatedBy = dto.CreatedBy,
                SourceLocation = dto.SourceLocation,
                DestinationLocation = dto.DestinationLocation,
                CreatedAt = DateTime.UtcNow,
                Status = VoucherStatus.Pending,

                Reason = $"{dto.Reason} | EquipmentId:{dto.EquipmentId}"
            };

            await _transferRepo.AddAsync(voucher);
            return MapToDto(voucher);
        }

        public async Task<TransferResponseDTO> ApproveRequest(string id, ApproveTransferDTO dto)
        {
            var voucher = await _transferRepo.GetByIdAsync(id);
            if (voucher == null) throw new Exception("Phiếu chuyển không tồn tại.");

            if (voucher.Status != VoucherStatus.Pending)
                throw new Exception("Phiếu này đã được xử lý rồi.");

            voucher.ApprovedBy = dto.ApproverId;
            voucher.ApprovedAt = DateTime.UtcNow;

            if (!dto.IsApproved)
            {
                voucher.Status = VoucherStatus.Denied;
            }
            else
            {
                var parts = voucher.Reason?.Split("| EquipmentId:");
                var equipId = parts?.Length > 1 ? parts[1].Trim() : null;

                if (string.IsNullOrEmpty(equipId))
                    throw new Exception("Equipment ID not found in transfer slip.");

                var equipment = await _equipmentRepo.GetEquipmentByIdAsync(equipId);
                if (equipment == null)
                    throw new Exception("The equipment to be transferred no longer exists.");

                equipment.RoomId = voucher.DestinationLocation;

                await _equipmentRepo.UpdateEquipmentAsync(equipment);

                voucher.Status = VoucherStatus.Accepted;
            }

            voucher.StatusUpdatedBy = dto.ApproverId;
            voucher.StatusUpdatedAt = DateTime.UtcNow;

            await _transferRepo.UpdateAsync(voucher);
            return MapToDto(voucher);
        }

        public async Task<TransferResponseDTO?> GetById(string id)
        {
            var voucher = await _transferRepo.GetByIdAsync(id);
            return voucher == null ? null : MapToDto(voucher);
        }

        // Helper Map
        private TransferResponseDTO MapToDto(TransferVoucher v)
        {
            var parts = v.Reason?.Split("| EquipmentId:");
            var reasonReal = parts?.Length > 0 ? parts[0].Trim() : v.Reason;
            var equipId = parts?.Length > 1 ? parts[1].Trim() : "";

            return new TransferResponseDTO
            {
                TransferId = v.TransferId,
                EquipmentId = equipId,
                Reason = reasonReal,
                SourceLocation = v.SourceLocation,
                DestinationLocation = v.DestinationLocation,
                Status = v.Status.ToString(),
                CreatedBy = v.CreatedBy,
                CreatedAt = v.CreatedAt,
                ApprovedBy = v.ApprovedBy,
                ApprovedAt = v.ApprovedAt
            };
        }
    }
}