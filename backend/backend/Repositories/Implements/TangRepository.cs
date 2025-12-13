using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class TangRepository : ITangRepository
    {
        private readonly DataApplicationDbContext _context;

        public TangRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tang?> getTangByIdAsync(string maTang)
        {
            return await _context.Tang.FindAsync(maTang);
        }

        public async Task<IEnumerable<Tang>> getAllTangAsync()
        {
            return await _context.Tang.ToListAsync();
        }

        public async Task<IEnumerable<Tang>> getAllTangOfToaAsync(string maToa)
        {
            return await _context.Tang
                         .Where(t => t.maToa == maToa)
                         .ToListAsync();
        }

        public async Task AddTangAsync(Tang tang)
        {
            await _context.Tang.AddAsync(tang);
        }

        public async Task RemoveTangAsync(Tang tang)
        {
            _context.Tang.Remove(tang);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
