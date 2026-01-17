using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IImportRequestService
    {
        Task<PageVO<ImportRequestResponse>> GetAll(EntityFilter<ImportRequest> filter, EntitySort<ImportRequest> sort, int page, int size);
        Task<ImportRequestResponse?> GetById(string id);
        Task<ImportRequestResponse> Create(CreateImportRequestRequest request);
        //Task<ImportRequestResponse> Update(string id, UpdateImportRequestRequest request);
        Task UpdateStatus(string id, UpdateImportRequestStatusRequest request);
        Task Delete(string id);
    }
}