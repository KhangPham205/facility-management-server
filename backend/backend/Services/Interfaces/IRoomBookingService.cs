using backend.DTOs.Booking.Request;
using backend.DTOs.Booking.Response;
using backend.Models.Area;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Interfaces
{
    public interface IRoomBookingService
    {
        Task<PageVO<RoomBookingResponse>> GetAll(EntityFilter<RoomBooking> filter, EntitySort<RoomBooking> sort, int page, int size);
        Task<RoomBookingResponse> Create(CreateBookingRequest request);
        Task<RoomBookingResponse> Approve(string id, ApproveBookingRequest request);
        Task Cancel(string id); // Người dùng tự hủy
    }
}