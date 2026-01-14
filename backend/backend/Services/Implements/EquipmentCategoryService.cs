using AutoMapper;
using backend.DTOs.EquipmentCategory.Request;
using backend.DTOs.EquipmentCategory.Response;
using backend.Exceptions;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class EquipmentCategoryService : IEquipmentCategoryService
    {
        private readonly IEquipmentCategoryRepository _repo;
        private readonly IMapper _mapper;

        public EquipmentCategoryService(IEquipmentCategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<EquipmentCategoryResponse>> GetAll(EntityFilter<EquipmentCategory> filter, EntitySort<EquipmentCategory> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<EquipmentCategoryResponse>>(pagedResult.Content);

            return new PageVO<EquipmentCategoryResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<EquipmentCategoryResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<EquipmentCategoryResponse>(entity);
        }

        public async Task<EquipmentCategoryResponse> Create(CreateEquipmentCategoryRequest request)
        {
            var entity = _mapper.Map<EquipmentCategory>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<EquipmentCategoryResponse>(entity);
        }

        public async Task<EquipmentCategoryResponse> Update(string id, UpdateEquipmentCategoryRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<EquipmentCategoryResponse>(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}