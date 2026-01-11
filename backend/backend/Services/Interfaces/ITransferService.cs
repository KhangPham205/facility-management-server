using backend.DTOs.Transfer;
using backend.vo;

namespace backend.Services.Interfaces
{
    public interface ITransferService
    {
        Task<PageVO<TransferResponseDTO>> GetAll(int page, int size);
        Task<TransferResponseDTO> CreateRequest(CreateTransferDTO dto);
        Task<TransferResponseDTO> ApproveRequest(string id, ApproveTransferDTO dto);
        Task<TransferResponseDTO?> GetById(string id);
    }
}
