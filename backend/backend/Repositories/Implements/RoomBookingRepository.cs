using backend.Data;
using backend.Enums;
using backend.Models.Area;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class RoomBookingRepository : IRoomBookingRepository
    {
        private readonly DataApplicationDbContext _context;

        public RoomBookingRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageVO<RoomBooking>> GetPagedAsync(EntityFilter<RoomBooking> filter, EntitySort<RoomBooking> sort, int page, int size)
        {
            var query = _context.RoomBookings
                .Include(x => x.Room)
                .Include(x => x.Borrower)
                .Include(x => x.Approver)
                .AsQueryable();

            query = query.Where(filter).OrderBy(sort);
            var total = await query.CountAsync();
            var content = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PageVO<RoomBooking>(page, size, total, content);
        }

        public async Task<RoomBooking?> GetByIdAsync(string id)
        {
            return await _context.RoomBookings
                .Include(x => x.Room)
                .Include(x => x.Borrower)
                .Include(x => x.Approver)
                .FirstOrDefaultAsync(x => x.BookingId == id);
        }

        public async Task AddAsync(RoomBooking booking)
        {
            await _context.RoomBookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomBooking booking)
        {
            _context.RoomBookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckAvailabilityAsync(string roomId, DateTime start, DateTime end, string? excludeBookingId = null)
        {
            // Logic trùng lịch: (StartA < EndB) && (EndA > StartB)
            // Chỉ kiểm tra các đơn ĐÃ DUYỆT (Approved). Đơn Pending có thể trùng, ai duyệt trước người đó được.
            var query = _context.RoomBookings.AsQueryable();

            if (!string.IsNullOrEmpty(excludeBookingId))
            {
                query = query.Where(x => x.BookingId != excludeBookingId);
            }

            return !await query.AnyAsync(x =>
                x.RoomId == roomId &&
                x.Status == BookingStatus.Approved && // Chỉ check lịch đã chốt
                x.StartTime < end &&
                x.EndTime > start);
        }
    }
}