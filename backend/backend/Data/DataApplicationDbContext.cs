using backend.Models.User;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DataApplicationDbContext : DbContext
    {
        public DataApplicationDbContext(DbContextOptions<DataApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
