using AutoMapper;
using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Models.Liquidate;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;
using backend.Utils;

namespace backend.Services.Implements
{
    public class LiquidateVoucherService : ILiquidateVoucherService
    {
        private readonly ILiquidateVoucherRepository _repo;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public LiquidateVoucherService(ILiquidateVoucherRepository repo, IMapper mapper, JwtUtils jwtUtils)
        {
            _repo = repo;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        public async Task<PageVO<LiquidateVoucherResponse>> GetAll(EntityFilter<LiquidateVoucher> filter, EntitySort<LiquidateVoucher> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<LiquidateVoucherResponse>>(pagedResult.Content);

            return new PageVO<LiquidateVoucherResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<LiquidateVoucherResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<LiquidateVoucherResponse>(entity);
        }

        public async Task<LiquidateVoucherResponse> Create(CreateLiquidateVoucherRequest request)
        {
            var entity = _mapper.Map<LiquidateVoucher>(request);

            entity.CreatedBy = _jwtUtils.GetCurrentUserId();

            await _repo.AddAsync(entity);
            return _mapper.Map<LiquidateVoucherResponse>(entity);
        }

        //public async Task<LiquidateVoucherResponse> Update(string id, UpdateLiquidateVoucherRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No import voucher found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<LiquidateVoucherResponse>(entity);
        //}

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No liquidate voucher found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}