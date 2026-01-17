using AutoMapper;
using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class ImportRequestService : IImportRequestService
    {
        private readonly IImportRequestRepository _repo;
        private readonly IMapper _mapper;

        public ImportRequestService(IImportRequestRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<ImportRequestResponse>> GetAll(EntityFilter<ImportRequest> filter, EntitySort<ImportRequest> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<ImportRequestResponse>>(pagedResult.Content);

            return new PageVO<ImportRequestResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<ImportRequestResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ImportRequestResponse>(entity);
        }

        public async Task<ImportRequestResponse> Create(CreateImportRequestRequest request)
        {
            var entity = _mapper.Map<ImportRequest>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<ImportRequestResponse>(entity);
        }

        //public async Task<ImportRequestResponse> Update(string id, UpdateImportRequestRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<ImportRequestResponse>(entity);
        //}

        public async Task UpdateStatus(string id, UpdateImportRequestStatusRequest request)
        {
            var result = await _repo.UpdateStatusAsync(id, request.Status);

            if (!result)
            {
                throw new NotFoundException($"No equipment found with ID: {id}");
            }
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}