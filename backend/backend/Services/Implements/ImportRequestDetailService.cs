using AutoMapper;
using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Utils;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class ImportRequestDetailService : IImportRequestDetailService
    {
        private readonly IImportRequestDetailRepository _repo;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public ImportRequestDetailService(IImportRequestDetailRepository repo, IMapper mapper, JwtUtils jwtUtils)
        {
            _repo = repo;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        public async Task<PageVO<ImportRequestDetailResponse>> GetAll(EntityFilter<ImportRequestDetail> filter, EntitySort<ImportRequestDetail> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<ImportRequestDetailResponse>>(pagedResult.Content);

            return new PageVO<ImportRequestDetailResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<ImportRequestDetailResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ImportRequestDetailResponse>(entity);
        }

        //public async Task<ImportRequestDetailResponse> Create(CreateImportRequestDetailRequest request)
        //{
        //    var entity = _mapper.Map<ImportRequestDetail>(request);
        //    entity.ApprovedBy = _jwtUtils.GetCurrentUserId();
        //    await _repo.AddAsync(entity);
        //    return _mapper.Map<ImportRequestDetailResponse>(entity);
        //}

        //public async Task<ImportRequestDetailResponse> Update(string id, UpdateImportRequestDetailRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<ImportRequestDetailResponse>(entity);
        //}

        //public async Task UpdateStatus(string id, UpdateImportRequestDetailStatusRequest request)
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