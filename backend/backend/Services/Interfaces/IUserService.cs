using backend.DTOs.user;
using backend.vo;

namespace backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<PageVO<UserResponseDTO>> GetUsers(int page, int size);
        Task<UserResponseDTO?> GetUserById(string id);
        Task<UserResponseDTO> CreateUser(CreateUserDTO dto);
        Task<UserResponseDTO> UpdateUser(string id, UpdateUserDTO dto);
        Task DeleteUser(string id);
    }
}
