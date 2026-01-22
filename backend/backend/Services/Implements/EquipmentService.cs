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
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;

        public EquipmentService(IEquipmentRepository repo, IAreaRepository areaRepository, IMapper mapper)
        {
            _repo = repo;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<PageVO<EquipmentResponse>> GetAll(EntityFilter<Equipment> filter, EntitySort<Equipment> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<EquipmentResponse>>(pagedResult.Content);

            if (dtoList.Any())
            {
                var buildingIds = dtoList.Where(d => d.LocationType == LocationType.Building).Select(d => d.LocationId).Distinct().ToList();
                var floorIds = dtoList.Where(d => d.LocationType == LocationType.Floor).Select(d => d.LocationId).Distinct().ToList();
                var roomIds = dtoList.Where(d => d.LocationType == LocationType.Room).Select(d => d.LocationId).Distinct().ToList();

                var buildingMap = await _areaRepository.GetNamesByIdsAsync(LocationType.Building, buildingIds);
                var floorMap = await _areaRepository.GetNamesByIdsAsync(LocationType.Floor, floorIds);
                var roomMap = await _areaRepository.GetNamesByIdsAsync(LocationType.Room, roomIds);

                foreach (var d in dtoList)
                {
                    d.LocationName = d.LocationType switch
                    {
                        LocationType.Building => buildingMap.GetValueOrDefault(d.LocationId),
                        LocationType.Floor => floorMap.GetValueOrDefault(d.LocationId),
                        LocationType.Room => roomMap.GetValueOrDefault(d.LocationId),
                        _ => "N/A"
                    };
                }
            }

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
            var response = _mapper.Map<EquipmentResponse>(entity);
            response.LocationName = await _areaRepository.GetNameByIdAsync(response.LocationType, response.LocationId);
            return response;
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