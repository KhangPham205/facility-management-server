using backend.DTOs.FundSource.Request;
using backend.DTOs.FundSource.Response;
using backend.Models.Finance;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IFundSourceService
    {
        Task<PageVO<FundSourceResponse>> GetAll(EntityFilter<FundSource> filter, EntitySort<FundSource> sort, int page, int size);
        Task<FundSourceResponse?> GetById(string id);
        Task<FundSourceResponse> Create(CreateFundSourceRequest request);
        Task<FundSourceResponse> Update(string id, UpdateFundSourceRequest request);
        Task Delete(string id);
    }
}
