using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IImportRequestDetailService
    {
        Task<PageVO<ImportRequestDetailResponse>> GetAll(EntityFilter<ImportRequestDetail> filter, EntitySort<ImportRequestDetail> sort, int page, int size);
        Task<ImportRequestDetailResponse?> GetById(string id);
        //Task<ImportRequestDetailResponse> Create(CreateImportRequestDetailRequest request);
        //Task<ImportRequestDetailResponse> Update(string id, UpdateImportRequestDetailRequest request);
        //Task UpdateStatus(string id, UpdateImportRequestDetailStatusRequest request);
        Task Delete(string id);
    }
}