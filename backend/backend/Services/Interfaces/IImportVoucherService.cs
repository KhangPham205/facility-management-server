using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Models;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IImportVoucherService
    {
        Task<PageVO<ImportVoucherResponse>> GetAll(EntityFilter<ImportVoucher> filter, EntitySort<ImportVoucher> sort, int page, int size);
        Task<ImportVoucherResponse?> GetById(string id);
        Task<ImportVoucherResponse> Create(CreateImportVoucherRequest request);
        //Task<ImportVoucherResponse> Update(string id, UpdateImportVoucherRequest request);
        Task Delete(string id);
    }
}