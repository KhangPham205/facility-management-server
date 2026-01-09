using backend.DTOs.Building.Request;
using backend.DTOs.Building.Response;
using backend.Mapping;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services.Implements
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        // LẤY TẤT CẢ TÒA NHÀ
        public async Task<IEnumerable<BuildingResponse>> GetAllBuildingsAsync()
        {
            var buildings = await _buildingRepository.GetAllAsync();

            // Sử dụng BuildingMapper để chuyển đổi danh sách
            return buildings.Select(b => BuildingMapper.ToBuildingResponse(b)).ToList();
        }

        // LẤY TÒA NHÀ THEO ID
        public async Task<BuildingResponse?> GetBuildingByIdAsync(string id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);

            if (building == null)
            {
                return null;
            }

            return BuildingMapper.ToBuildingResponse(building);
        }

        // TẠO MỚI TÒA NHÀ
        public async Task<BuildingResponse> CreateBuildingAsync(BuildingCreationRequest request)
        {
            // Chuyển Request DTO sang Entity
            var newBuilding = BuildingMapper.ToEntityFromCreateRequest(request);

            // Gán ID mới
            newBuilding.BuildingId = Guid.NewGuid().ToString();

            await _buildingRepository.AddAsync(newBuilding);
            await _buildingRepository.SaveChangesAsync();

            return BuildingMapper.ToBuildingResponse(newBuilding);
        }

        // CẬP NHẬT TÒA NHÀ
        public async Task<BuildingResponse?> UpdateBuildingAsync(string id, BuildingUpdateRequest request)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var building = await _buildingRepository.GetByIdAsync(id);

            if (building == null)
            {
                return null;
            }

            // Cập nhật thông tin vào Entity hiện tại
            BuildingMapper.UpdateEntityFromRequest(request, building);

            var success = await _buildingRepository.SaveChangesAsync();

            if (!success)
            {
                return BuildingMapper.ToBuildingResponse(building);
            }

            return BuildingMapper.ToBuildingResponse(building);
        }

        // XÓA TÒA NHÀ
        public async Task<bool> DeleteBuildingAsync(string id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);

            if (building == null)
            {
                return false;
            }

            await _buildingRepository.DeleteAsync(building);
            return await _buildingRepository.SaveChangesAsync();
        }
    }
}