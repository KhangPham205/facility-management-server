using backend.Data;
using backend.Models.Finance;
using backend.Repositories.Interfaces;
using backend.vo;
using Microsoft.EntityFrameworkCore;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Repositories.Implements
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataApplicationDbContext _context;
        public InvoiceRepository(DataApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PageVO<Invoice>> GetPagedAsync(EntityFilter<Invoice> filter, EntitySort<Invoice> sort, int page, int size)
        {
            var query = _context.Invoices.AsQueryable();
            // Apply filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }
            // Apply sorting
            if (sort != null)
            {
                query = query.OrderBy(sort);
            }
            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PageVO<Invoice>(page, size, totalItems, items);
        }
        public async Task<Invoice?> GetByIdAsync(string id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Unit)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
            return invoice;
        }
        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
        /*public async Task UpdateAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }*/
        public async Task DeleteAsync(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
