using backend.DTOs.Criteria.Request;
using backend.DTOs.Criteria.Response;
using backend.Models.EquipmentInfo;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface ICriteriaService
    {
        Task<PageVO<CriteriaResponse>> GetAll(EntityFilter<Criteria> filter, EntitySort<Criteria> sort, int page, int size);
        Task<CriteriaResponse?> GetById(string id);
        Task<CriteriaResponse> Create(CreateCriteriaRequest request);
        Task<List<CriteriaResponse>> CreateList(CreateCriteriaListRequest request);
        Task<CriteriaResponse> Update(string id, UpdateCriteriaRequest request);
        Task Delete(string id);
    }
}