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
    public class LiquidateRequestService : ILiquidateRequestService
    {
        private readonly ILiquidateRequestRepository _repo;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public LiquidateRequestService(ILiquidateRequestRepository repo, IMapper mapper, JwtUtils jwt)
        {
            _repo = repo;
            _mapper = mapper;
            _jwtUtils = jwt;
        }

        public async Task<PageVO<LiquidateRequestResponse>> GetAll(EntityFilter<LiquidateRequest> filter, EntitySort<LiquidateRequest> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<LiquidateRequestResponse>>(pagedResult.Content);

            return new PageVO<LiquidateRequestResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<LiquidateRequestResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<LiquidateRequestResponse>(entity);
        }

        public async Task<LiquidateRequestResponse> Create(CreateLiquidateRequestRequest request)
        {
            var entity = _mapper.Map<LiquidateRequest>(request);

            entity.CreatedBy = _jwtUtils.GetCurrentUserId();

            await _repo.AddAsync(entity);
            return _mapper.Map<LiquidateRequestResponse>(entity);
        }

        //public async Task<LiquidateRequestResponse> Update(string id, UpdateLiquidateRequestRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<LiquidateRequestResponse>(entity);
        //}

        public async Task UpdateStatus(string id, UpdateLiquidateRequestStatusRequest request)
        {
            var result = await _repo.UpdateStatusAsync(id, _jwtUtils.GetCurrentUserId(), request.Status);

            if (!result)
            {
                throw new NotFoundException($"No liquidate request found with ID: {id}");
            }
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No liquidate request found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}