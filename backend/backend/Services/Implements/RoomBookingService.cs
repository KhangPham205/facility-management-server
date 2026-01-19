using AutoMapper;
using backend.DTOs.Booking.Request;
using backend.DTOs.Booking.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Utils;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly IRoomBookingRepository _repo;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public RoomBookingService(IRoomBookingRepository repo, JwtUtils jwtUtils, IMapper mapper)
        {
            _repo = repo;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<PageVO<RoomBookingResponse>> GetAll(EntityFilter<RoomBooking> filter, EntitySort<RoomBooking> sort, int page, int size)
        {
            var paged = await _repo.GetPagedAsync(filter, sort, page, size);
            var dtos = _mapper.Map<List<RoomBookingResponse>>(paged.Content);
            return new PageVO<RoomBookingResponse>(paged.Page, paged.Size, paged.TotalElements, dtos);
        }

        public async Task<RoomBookingResponse> Create(CreateBookingRequest request)
        {
            if (request.StartTime >= request.EndTime)
                throw new BadRequestException("The start time must be shorter than the end time.");

            if (request.StartTime < DateTime.Now)
                throw new BadRequestException("Reservations cannot be made for past dates.");

            var isAvailable = await _repo.CheckAvailabilityAsync(request.RoomId, request.StartTime, request.EndTime);
            if (!isAvailable)
                throw new BadRequestException("The room is already booked for this period.");

            var booking = new RoomBooking
            {
                RoomId = request.RoomId,
                BorrowerId = _jwtUtils.GetCurrentUserId(),
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Purpose = request.Purpose,
                Status = BookingStatus.Pending
            };

            await _repo.AddAsync(booking);

            var created = await _repo.GetByIdAsync(booking.BookingId);
            return _mapper.Map<RoomBookingResponse>(created);
        }

        public async Task<RoomBookingResponse> Approve(string id, ApproveBookingRequest request)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) throw new NotFoundException("Không tìm thấy đơn đặt phòng.");

            if (booking.Status != BookingStatus.Pending)
                throw new BadRequestException("Đơn này đã được xử lý trước đó.");

            var currentUserId = _jwtUtils.GetCurrentUserId();

            if (request.IsApproved)
            {
                var isAvailable = await _repo.CheckAvailabilityAsync(booking.RoomId, booking.StartTime, booking.EndTime, booking.BookingId);
                if (!isAvailable)
                    throw new BadRequestException("Scheduling conflict! This room has just been approved for another application in the same time slot.");

                booking.Status = BookingStatus.Approved;
            }
            else
            {
                booking.Status = BookingStatus.Rejected;
            }

            booking.ApprovedBy = currentUserId;
            booking.ApprovedAt = DateTime.Now;
            booking.Note = request.Note;

            await _repo.UpdateAsync(booking);
            return _mapper.Map<RoomBookingResponse>(booking);
        }

        public async Task Cancel(string id)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) throw new NotFoundException("No booking found.");

            var currentUserId = _jwtUtils.GetCurrentUserId();

            if (booking.BorrowerId != currentUserId)
                throw new UnauthorizedException("You do not have the right to cancel this order.");

            if (booking.Status == BookingStatus.Completed || booking.Status == BookingStatus.Cancelled)
                throw new BadRequestException("The order cannot be canceled.");

            booking.Status = BookingStatus.Cancelled;
            await _repo.UpdateAsync(booking);
        }
    }
}