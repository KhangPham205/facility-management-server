using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Models.Import;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IImportVoucherDetailService
    {
        Task<PageVO<ImportVoucherDetailResponse>> GetAll(EntityFilter<ImportVoucherDetail> filter, EntitySort<ImportVoucherDetail> sort, int page, int size);
        Task<ImportVoucherDetailResponse?> GetById(string id);
        //Task<ImportVoucherDetailResponse> Create(CreateImportVoucherDetailVoucher request);
        //Task<ImportVoucherDetailResponse> Update(string id, UpdateImportVoucherDetailVoucher request);
        //Task UpdateStatus(string id, UpdateImportVoucherDetailStatusVoucher request);
        Task Delete(string id);
    }
}