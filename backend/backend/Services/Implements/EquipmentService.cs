using AutoMapper;
using backend.DTOs.Equipment.Request;
using backend.DTOs.Equipment.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.EquipmentInfo;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _repo;
        private readonly IMapper _mapper;

        public EquipmentService(IEquipmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<EquipmentResponse>> GetAll(EntityFilter<Equipment> filter, EntitySort<Equipment> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<EquipmentResponse>>(pagedResult.Content);

            return new PageVO<EquipmentResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<EquipmentResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<EquipmentResponse>(entity);
        }

        public async Task<EquipmentResponse> Create(CreateEquipmentRequest request)
        {
            var entity = _mapper.Map<Equipment>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<EquipmentResponse>(entity);
        }

        public async Task<EquipmentResponse> Update(string id, UpdateEquipmentRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No equipment found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<EquipmentResponse>(entity);
        }

        public async Task UpdateStatus(string id, UpdateEquipmentStatusRequest request)
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