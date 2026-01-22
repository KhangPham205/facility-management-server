using AutoMapper;
using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.DTOs.Equipment.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Models.Audit;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class InventoryAuditService : IInventoryAuditService
    {
        private readonly IInventoryAuditRepository _repo;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;

        public InventoryAuditService(IInventoryAuditRepository repo, IAreaRepository areaRepository, IMapper mapper)
        {
            _repo = repo;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<PageVO<InventoryAuditResponse>> GetAll(EntityFilter<InventoryAudit> filter, EntitySort<InventoryAudit> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<InventoryAuditResponse>>(pagedResult.Content);

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
            return new PageVO<InventoryAuditResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<InventoryAuditResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            var response = _mapper.Map<InventoryAuditResponse>(entity);
            response.LocationName = await _areaRepository.GetNameByIdAsync(response.LocationType, response.LocationId);
            return response;
        }

        public async Task<InventoryAuditResponse> Create(CreateInventoryAuditRequest request)
        {
            var entity = _mapper.Map<InventoryAudit>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<InventoryAuditResponse>(entity);
        }

        //public async Task<InventoryAuditResponse> Update(string id, UpdateInventoryAuditRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No room found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<InventoryAuditResponse>(entity);
        //}

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No inventory audit found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}