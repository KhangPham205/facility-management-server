using backend.Data;
using backend.Models;
using backend.Models.Phong;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class PhongRepository : IPhongRepository
    {
        private readonly DataApplicationDbContext _context;

        public PhongRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Phong?> GetPhongByIdAsync(string maTang)
        {
            return await _context.Phong.FindAsync(maTang);
        }

        public async Task<IEnumerable<Phong>> GetAllPhongAsync()
        {
            return await _context.Phong.ToListAsync();
        }

        public async Task<IEnumerable<Phong>> GetAllPhongOfToaAsync(string maToa)
        {
            return await _context.Phong
                .Include(p=>p.tang)
                .Where(p=>p.tang.maToa == maToa)
                .ToListAsync();
        }

        public async Task<IEnumerable<Phong>> GetAllPhongOfTangAsync(string maTang)
        {
            return await _context.Phong
                .Where(p => p.maTang == maTang)
                .ToListAsync();
        }

        public async Task AddPhongAsync(Phong phong)
        {
            await _context.Phong.AddAsync(phong);
        }

        public async Task RemovePhongAsync(Phong phong)
        {
            _context.Phong.Remove(phong);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
