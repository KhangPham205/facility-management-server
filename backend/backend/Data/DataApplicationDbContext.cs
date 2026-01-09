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

        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        //public DbSet<ThoiKhoaBieu> ThoiKhoaBieu { get; set; }
        //public DbSet<ThietBi> ThietBi { get; set; }
        //public DbSet<PhieuNhap> PhieuNhap { get; set; }
        //public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        //public DbSet<PhieuXuat> PhieuXuat { get; set; }
        //public DbSet<ChiTietPhieuXuat> ChiTietPhieuXuat { get; set; }
        //public DbSet<PhieuChuyen> PhieuChuyen { get; set; }
        //public DbSet<ChiTietPhieuChuyen> ChiTietPhieuChuyen { get; set; }
        //public DbSet<PhieuMuon> PhieuMuon { get; set; }
        //public DbSet<ChiTietPhieuMuon> ChiTietPhieuMuon { get; set; }
        //public DbSet<PhieuViPhamMuon> PhieuViPhamMuon { get; set; }
        //public DbSet<YeuCauBaoTri> YeuCauBaoTri { get; set; }
        //public DbSet<PhieuBaoTri> PhieuBaoTri { get; set; }
        //public DbSet<PhieuBaoHong> PhieuBaoHong { get; set; }
        //public DbSet<PhieuSuaChua> PhieuSuaChua { get; set; }
        //public DbSet<PhieuKiemKe> PhieuKiemKe { get; set; }
        //public DbSet<ChiTietKiemKe> ChiTietKiemKe { get; set; }
        //public DbSet<DonViNgoaiTruong> DonViNgoaiTruong { get; set; }
        //public DbSet<LoaiPhong> LoaiPhong { get; set; }
        //public DbSet<NguonChiPhi> NguonChiPhi { get; set; }
        //public DbSet<HoaDon> HoaDon { get; set; }
        //public DbSet<KiemKeDinhKy> KiemKeDinhKy { get; set; }
        //public DbSet<LoaiDonVi> LoaiDonVi { get; set; }
        //public DbSet<LoaiThietBi> LoaiThietBi { get; set; }
    }
}