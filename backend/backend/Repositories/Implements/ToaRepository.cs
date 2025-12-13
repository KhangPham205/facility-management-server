using backend.Models;
using backend.Repositories.Interfaces;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implements
{
    public class ToaRepository : IToaRepository
    {
        private readonly DataApplicationDbContext _context;

        public ToaRepository(DataApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Toa>> GetAllAsync()
        {
            return await _context.Toa.ToListAsync();
        }

        public async Task<Toa?> GetByIdAsync(string maToa)
        {
            return await _context.Toa.FindAsync(maToa);
        }

        public async Task AddAsync(Toa toa)
        {
            await _context.Toa.AddAsync(toa);
        }

        public async Task DeleteAsync(Toa toa)
        {
            _context.Toa.Remove(toa);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
