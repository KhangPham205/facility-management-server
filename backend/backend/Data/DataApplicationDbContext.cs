using Microsoft.EntityFrameworkCore;
using backend.Models.TaiKhoan;
using backend.Models.PhieuChuyen;

namespace backend.Data
{
    public class DataApplicationDbContext : DbContext
    {
        public DataApplicationDbContext(DbContextOptions<DataApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        //public DbSet<Toa> Toa { get; set; }
        //public DbSet<Tang> Tang { get; set; }
        //public DbSet<Phong> Phong { get; set; }
        //public DbSet<ThoiKhoaBieu> ThoiKhoaBieu { get; set; }
        //public DbSet<ThietBi> ThietBi { get; set; }
        //public DbSet<PhieuNhap> PhieuNhap { get; set; }
        //public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        //public DbSet<PhieuXuat> PhieuXuat { get; set; }
        //public DbSet<ChiTietPhieuXuat> ChiTietPhieuXuat { get; set; }
        public DbSet<PhieuChuyen> PhieuChuyen { get; set; }
        public DbSet<ChiTietPhieuChuyen> ChiTietPhieuChuyen { get; set; }
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
