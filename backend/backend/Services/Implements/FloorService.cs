using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;
using backend.Mapping;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services.Implements
{
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        // Lấy thông tin 1 tầng theo ID
        public async Task<FloorResponse?> GetFloorByIdAsync(string floorId)
        {
            var floor = await _floorRepository.GetFloorByIdAsync(floorId);

            if (floor == null) return null;

            return FloorMapper.ToFloorResponse(floor);
        }

        // Lấy tất cả các tầng
        public async Task<IEnumerable<FloorResponse>> GetAllFloorsAsync()
        {
            var floors = await _floorRepository.GetAllFloorsAsync();
            return floors.Select(f => FloorMapper.ToFloorResponse(f)).ToList();
        }

        // Lấy tất cả các tầng thuộc một tòa nhà cụ thể
        public async Task<IEnumerable<FloorResponse>> GetFloorsByBuildingIdAsync(string buildingId)
        {
            var floors = await _floorRepository.GetFloorsByBuildingIdAsync(buildingId);
            return floors.Select(f => FloorMapper.ToFloorResponse(f)).ToList();
        }

        // Tạo mới một tầng
        public async Task<FloorResponse?> CreateFloorAsync(FloorCreationRequest request)
        {
            // Chuyển đổi DTO sang Entity
            var newFloor = FloorMapper.ToFloorEntity(request);

            if (string.IsNullOrEmpty(newFloor.FloorId))
            {
                newFloor.FloorId = Guid.NewGuid().ToString();
            }

            await _floorRepository.AddFloorAsync(newFloor);
            bool isSaved = await _floorRepository.SaveChangesAsync();

            if (!isSaved) return null;

            return FloorMapper.ToFloorResponse(newFloor);
        }

        // Cập nhật thông tin tầng
        public async Task<FloorResponse?> UpdateFloorAsync(string floorId, FloorUpdateRequest request)
        {
            var existingFloor = await _floorRepository.GetFloorByIdAsync(floorId);

            if (existingFloor == null) return null;

            // Cập nhật dữ liệu từ request vào entity hiện tại
            FloorMapper.UpdateFloorFromRequest(request, existingFloor);

            bool isSaved = await _floorRepository.SaveChangesAsync();

            if (!isSaved) return null;

            return FloorMapper.ToFloorResponse(existingFloor);
        }

        // Xóa tầng
        public async Task<bool> DeleteFloorAsync(string floorId)
        {
            var floor = await _floorRepository.GetFloorByIdAsync(floorId);

            if (floor == null) return false;

            await _floorRepository.RemoveFloorAsync(floor);
            return await _floorRepository.SaveChangesAsync();
        }
    }
}