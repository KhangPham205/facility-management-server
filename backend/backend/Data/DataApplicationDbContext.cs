using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data // (Check namespace của bạn)
{
    public class DataApplicationDbContext : DbContext
    {
        public DataApplicationDbContext(DbContextOptions<DataApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FundSource> FundSources { get; set; }
        public DbSet<BorrowVoucher> BorrowVouchers { get; set; }
        public DbSet<BorrowDetail> BorrowDetails { get; set; }
        public DbSet<TransferVoucher> TransferVouchers { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BorrowDetail>()
                .HasKey(bd => new { bd.BorrowId, bd.EquipmentId });

            modelBuilder.Entity<BorrowDetail>()
                .HasOne(bd => bd.BorrowVoucher)
                .WithMany(bv => bv.BorrowDetails)
                .HasForeignKey(bd => bd.BorrowId);
        }
    }
}