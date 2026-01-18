using AutoMapper;
using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Liquidate;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Utils;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class LiquidateVoucherDetailService : ILiquidateVoucherDetailService
    {
        private readonly ILiquidateVoucherDetailRepository _repo;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public LiquidateVoucherDetailService(ILiquidateVoucherDetailRepository repo, IMapper mapper, JwtUtils jwtUtils)
        {
            _repo = repo;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        public async Task<PageVO<LiquidateVoucherDetailResponse>> GetAll(EntityFilter<LiquidateVoucherDetail> filter, EntitySort<LiquidateVoucherDetail> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<LiquidateVoucherDetailResponse>>(pagedResult.Content);

            return new PageVO<LiquidateVoucherDetailResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<LiquidateVoucherDetailResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<LiquidateVoucherDetailResponse>(entity);
        }

        //public async Task<LiquidateVoucherDetailResponse> Create(CreateLiquidateVoucherDetailVoucher request)
        //{
        //    var entity = _mapper.Map<LiquidateVoucherDetail>(request);
        //    entity.ApprovedBy = _jwtUtils.GetCurrentUserId();
        //    await _repo.AddAsync(entity);
        //    return _mapper.Map<LiquidateVoucherDetailResponse>(entity);
        //}

        //public async Task<LiquidateVoucherDetailResponse> Update(string id, UpdateLiquidateVoucherDetailVoucher request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<LiquidateVoucherDetailResponse>(entity);
        //}

        //public async Task UpdateStatus(string id, UpdateLiquidateVoucherDetailStatusVoucher request)
        //{
        //    var result = await _repo.UpdateStatusAsync(id, _jwtUtils.GetCurrentUserId(), request.Status);

        //    if (!result)
        //    {
        //        throw new NotFoundException($"No equipment found with ID: {id}");
        //    }
        //}

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}