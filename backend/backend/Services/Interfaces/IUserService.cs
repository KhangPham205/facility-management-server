using backend.DTOs.user.Request;
using backend.DTOs.user.Response;
using backend.Models;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<PageVO<UserResponseDTO>> GetUsers(EntityFilter<User> filter, EntitySort<User> sort, int page, int size);
        Task<UserResponseDTO?> GetUserById(string id);
        Task<UserResponseDTO> CreateUser(CreateUserDTO dto);
        Task<UserResponseDTO> UpdateUser(string id, UpdateUserDTO dto);
        Task DeleteUser(string id);
    }
}
