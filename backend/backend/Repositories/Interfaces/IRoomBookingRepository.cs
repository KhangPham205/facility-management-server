using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Interfaces
{
    public interface IRoomBookingRepository
    {
        Task<PageVO<RoomBooking>> GetPagedAsync(EntityFilter<RoomBooking> filter, EntitySort<RoomBooking> sort, int page, int size);
        Task<RoomBooking?> GetByIdAsync(string id);
        Task AddAsync(RoomBooking booking);
        Task UpdateAsync(RoomBooking booking);

        // Kiểm tra xem phòng có bị kẹt lịch Approved nào trong khoảng thời gian này không
        Task<bool> CheckAvailabilityAsync(string roomId, DateTime start, DateTime end, string? excludeBookingId = null);
    }
}
