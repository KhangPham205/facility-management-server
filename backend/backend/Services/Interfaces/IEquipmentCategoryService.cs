using backend.DTOs.EquipmentCategory.Request;
using backend.DTOs.EquipmentCategory.Response;
using backend.Models.EquipmentInfo;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IEquipmentCategoryService
    {
        Task<PageVO<EquipmentCategoryResponse>> GetAll(EntityFilter<EquipmentCategory> filter, EntitySort<EquipmentCategory> sort, int page, int size);
        Task<EquipmentCategoryResponse?> GetById(string id);
        Task<EquipmentCategoryResponse> Create(CreateEquipmentCategoryRequest request);
        Task<EquipmentCategoryResponse> Update(string id, UpdateEquipmentCategoryRequest request);
        Task Delete(string id);
    }
}