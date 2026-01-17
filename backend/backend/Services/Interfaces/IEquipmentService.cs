using backend.DTOs.Equipment.Request;
using backend.DTOs.Equipment.Response;
using backend.Enums;
using backend.Models.EquipmentInfo;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IEquipmentService
    {
        Task<PageVO<EquipmentResponse>> GetAll(EntityFilter<Equipment> filter, EntitySort<Equipment> sort, int page, int size);
        Task<EquipmentResponse?> GetById(string id);
        Task<EquipmentResponse> Create(CreateEquipmentRequest request);
        Task<EquipmentResponse> Update(string id, UpdateEquipmentRequest request);
        Task UpdateStatus(string id, UpdateEquipmentStatusRequest request);
        Task Delete(string id);
    }
}