using backend.Models.Finance;
using backend.vo;
using DTOs.ExternalUnit.Request;
using DTOs.ExternalUnit.Response;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IExternalUnitService
    {
        public Task<PageVO<ExternalUnitResponse>> GetAll(EntityFilter<ExternalUnit> filter, EntitySort<ExternalUnit> sort, int page, int size);
        public Task<ExternalUnitResponse?> GetById(string id);
        public Task<ExternalUnitResponse> Create(CreateExternalUnitRequest request);
        public Task<ExternalUnitResponse> Update(string id, UpdateExternalUnitRequest request);
        public Task Delete(string id);
    }
}
