using backend.Models;
using backend.Models.Area;
using backend.Models.Borrow;
using backend.Models.EquipmentInfo;
using backend.Models.Finance;
using backend.Models.Import;
using backend.Models.Liquidate;
using backend.Models.Maintenance;
using backend.Models.Repair;
using backend.Models.Transfer;
using Microsoft.EntityFrameworkCore;


namespace backend.Data // (Check namespace của bạn)
{
    public class DataApplicationDbContext : DbContext
    {
        public DataApplicationDbContext(DbContextOptions<DataApplicationDbContext> options) : base(options) { }

        // --- DbSets ---
        public DbSet<User> Users { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<FundSource> FundSources { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ExternalUnit> ExternalUnits { get; set; }

        // Import
        public DbSet<ImportRequest> ImportRequests { get; set; }
        public DbSet<ImportRequestDetail> ImportRequestDetails { get; set; }
        public DbSet<ImportVoucher> ImportVouchers { get; set; }
        public DbSet<ImportVoucherDetail> ImportVoucherDetails { get; set; }

        // Borrow
        public DbSet<BorrowVoucher> BorrowVouchers { get; set; }
        public DbSet<BorrowVoucherDetail> BorrowVoucherDetails { get; set; }

        // Transfer
        public DbSet<TransferRequest> TransferRequests { get; set; }
        public DbSet<TransferRequestDetail> TransferRequestDetails { get; set; }
        public DbSet<TransferVoucher> TransferVouchers { get; set; }
        public DbSet<TransferVoucherDetail> TransferVoucherDetails { get; set; }

        // Maintenance & Repair
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<MaintenanceRequestDetail> MaintenanceRequestDetails { get; set; }
        public DbSet<MaintenanceVoucher> MaintenanceVouchers { get; set; }
        public DbSet<MaintenanceVoucherDetail> MaintenanceVoucherDetails { get; set; }

        public DbSet<RepairRequest> RepairRequests { get; set; }
        public DbSet<RepairRequestDetail> RepairRequestDetails { get; set; }
        public DbSet<RepairVoucher> RepairVouchers { get; set; }

        // Liquidate
        public DbSet<LiquidateRequest> LiquidateRequests { get; set; }
        public DbSet<LiquidateRequestDetail> LiquidateRequestDetails { get; set; }
        public DbSet<LiquidateVoucher> LiquidateVouchers { get; set; }
        public DbSet<LiquidateVoucherDetail> LiquidateVoucherDetails { get; set; }

        // Audit
        public DbSet<PeriodicAudit> PeriodicAudits { get; set; }
        public DbSet<InventoryAudit> InventoryAudits { get; set; }
        public DbSet<AuditDetail> AuditDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Cấu hình Composite Keys (Khóa chính gồm 2 cột) ---

            // Import
            modelBuilder.Entity<ImportVoucherDetail>()
                .HasKey(x => new { x.ImportId, x.EquipmentId });

            // Borrow
            modelBuilder.Entity<BorrowVoucherDetail>()
                .HasKey(x => new { x.BorrowId, x.EquipmentId });

            // Transfer
            modelBuilder.Entity<TransferRequestDetail>()
                .HasKey(x => new { x.RequestId, x.EquipmentId });
            modelBuilder.Entity<TransferVoucherDetail>()
                .HasKey(x => new { x.TransferId, x.EquipmentId });

            // Maintenance
            modelBuilder.Entity<MaintenanceRequestDetail>()
                .HasKey(x => new { x.RequestId, x.EquipmentId });
            modelBuilder.Entity<MaintenanceVoucherDetail>()
                .HasKey(x => new { x.VoucherId, x.EquipmentId });

            // Repair
            modelBuilder.Entity<RepairRequestDetail>()
                .HasKey(x => new { x.RequestId, x.EquipmentId });
            modelBuilder.Entity<RepairVoucherDetail>()
                .HasKey(x => new { x.RepairId, x.EquipmentId });

            // Liquidate
            modelBuilder.Entity<LiquidateRequest>()
                .HasOne(r => r.Creator)
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LiquidateVoucher>()
                .HasOne(v => v.Creator)
                .WithMany()
                .HasForeignKey(v => v.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LiquidateRequestDetail>()
                .HasKey(x => new { x.RequestId, x.EquipmentId });
            modelBuilder.Entity<LiquidateVoucherDetail>()
                .HasKey(x => new { x.LiquidateId, x.EquipmentId });

            // --- Cấu hình Precision cho tiền tệ (Tránh lỗi decimal warning) ---
            modelBuilder.Entity<Equipment>().Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FundSource>().Property(p => p.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Invoice>().Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                // Nếu khóa ngoại trỏ đến bảng User, tắt xóa Cascade
                if (relationship.PrincipalKey.IsPrimaryKey() &&
                    relationship.PrincipalEntityType.ClrType == typeof(User))
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}