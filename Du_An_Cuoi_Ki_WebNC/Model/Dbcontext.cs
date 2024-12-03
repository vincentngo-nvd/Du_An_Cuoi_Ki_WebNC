using Microsoft.EntityFrameworkCore;
using NguyenVanCong_2307.Models;
namespace Du_An_Cuoi_Ki_WebNC.Model
{
    public class Dbcontext : DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> options) : base(options)
        {
        }
        public DbSet<sanpham> sanpham { get; set; }

        public DbSet<ThuongHieus> thuonghieu { get; set; }

        public DbSet<KhachHang> khachhang { get; set; }

        public DbSet<PhieuXuat> phieuxuat { get; set; }

        public DbSet<NhanvienModels> Nhanviens { get; set; }

        public DbSet<TaikhoanKH> taikhoans { get; set; }

        public DbSet<XuatXu> XuatXus { get; set; }

        public DbSet<PhienBanSanPham> PhienBanSanPhams { get; set; }

        public DbSet<MauSac> MauSacs { get; set; }

        public DbSet<HeDieuHanh> HeDieuHanhs { get; set; }

        public DbSet<DungLuongRom> DungLuongRoms { get; set; }

        public DbSet<DungLuongRam> DungLuongRams { get; set; }

        public DbSet<CTSanPham> CTSanPhams { get; set; }

        // Cấu hình bảng và các thuộc tính trong OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //--------------------------------------------------------------------------------

            // Cấu hình bảng XuatXu
            modelBuilder.Entity<XuatXu>()
                .HasKey(x => x.maXuatXu);
            modelBuilder.Entity<XuatXu>()
                .Property(x => x.tenXuatXu).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<XuatXu>()
                .Property(x => x.trangThai);

            //--------------------------------------------------------------------------------

            // Cấu hình bảng ThuongHieu
            modelBuilder.Entity<ThuongHieus>()
                .HasKey(t => t.mathuonghieu);
            modelBuilder.Entity<ThuongHieus>()
                .Property(t => t.tenthuonghieu).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<ThuongHieus>()
                .Property(t => t.trangthai);

            //--------------------------------------------------------------------------------

            // Cấu hình tên bảng PhienBanSanPham
            modelBuilder.Entity<sanpham>()
                .ToTable("sanpham");

            //--------------------------------------------------------------------------------

            // Cấu hình khóa chính của bảng SanPham
            modelBuilder.Entity<sanpham>()
                .HasKey(s => s.masp);

            //--------------------------------------------------------------------------------

            // Cấu hình các thuộc tính của bảng SanPham
            modelBuilder.Entity<sanpham>()
                .Property(s => s.tensp)
                .HasColumnName("tensp")  // Sửa tên cột thành tensp trong cơ sở dữ liệu
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.hinhanh).HasMaxLength(500);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.xuatxu).HasMaxLength(100);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.chipxuly).HasMaxLength(100);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.dungluongpin);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.kichthuocman);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.hedieuhanh).HasMaxLength(100);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.phienbanhdh)
                .HasColumnName("phienbanhdh");  // Sửa tên cột thành phienbanhdh trong cơ sở dữ liệu

            modelBuilder.Entity<sanpham>()
                .Property(s => s.camerasau).HasMaxLength(200);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.cameratruoc).HasMaxLength(200);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.thoigianbaohanh);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.thuonghieu).HasMaxLength(100);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.khuvuckho).HasMaxLength(100);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.soluongton);

            modelBuilder.Entity<sanpham>()
                .Property(s => s.trangthai);

            // Cấu hình quan hệ giữa bảng sanpham và ThuongHieus
            // Cấu hình mối quan hệ giữa sanpham và ThuongHieus
            modelBuilder.Entity<sanpham>()
                .HasOne(s => s.ThuongHieus)
                .WithMany(t => t.SanPham)
                .HasForeignKey(s => s.thuonghieu) // Thuộc tính FK trong sanpham
                .HasPrincipalKey(t => t.mathuonghieu); // Thuộc tính PK trong ThuongHieus


            //--------------------------------------------------------------------------------

            // Cấu hình tên bảng PhienBanSanPham
            modelBuilder.Entity<PhienBanSanPham>()
                .ToTable("phienbansanpham");

            //--------------------------------------------------------------------------------

            // Cấu hình khóa chính của bảng PhienBanSanPham
            modelBuilder.Entity<PhienBanSanPham>()
                .HasKey(p => p.maPhienBanSanPham);

            //--------------------------------------------------------------------------------

            // Cấu hình các thuộc tính của bảng PhienBanSanPham
            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.maSanPham);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.rom).HasMaxLength(100);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.ram).HasMaxLength(100);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.mauSac).HasMaxLength(100);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.giaNhap);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.giaXuat);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.soLuongTon);

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.trangThai);

            //--------------------------------------------------------------------------------

            // Cấu hình các mối quan hệ giữa bảng PhienBanSanPham và các bảng khác
            modelBuilder.Entity<PhienBanSanPham>()
                .HasOne(p => p.SanPham)
                .WithMany(s => s.PhienBanSanPhams)
                .HasForeignKey(p => p.maSanPham);

            modelBuilder.Entity<PhienBanSanPham>()
                .HasOne(p => p.DungLuongRom)
                .WithMany(d => d.PhienBanSanPhams)
                .HasForeignKey(p => p.rom)
                .HasPrincipalKey(d => d.maDungLuongRom);

            modelBuilder.Entity<PhienBanSanPham>()
                .HasOne(p => p.DungLuongRam)
                .WithMany(d => d.PhienBanSanPhams)
                .HasForeignKey(p => p.ram)
                .HasPrincipalKey(d => d.maDungLuongRam);

            //--------------------------------------------------------------------------------

            // Cập nhật tên cột trong cơ sở dữ liệu
            modelBuilder.Entity<sanpham>()
                .Property(s => s.masp)
                .HasColumnName("masp"); // Sửa tên cột thành masp trong cơ sở dữ liệu

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.maPhienBanSanPham)
                .HasColumnName("maphienbansp"); // Sửa tên cột thành maphienbansp trong cơ sở dữ liệu

            modelBuilder.Entity<PhienBanSanPham>()
                .Property(p => p.maSanPham)
                .HasColumnName("masp"); // Tên cột trong bảng PhienBanSanPham

            //--------------------------------------------------------------------------------

            // Cấu hình bảng MauSac
            modelBuilder.Entity<MauSac>()
                .HasKey(m => m.maMau);

            modelBuilder.Entity<MauSac>()
                .Property(m => m.tenMau).HasMaxLength(100);

            modelBuilder.Entity<MauSac>()
                .Property(m => m.trangThai);

            //--------------------------------------------------------------------------------

            // Cấu hình bảng HeDieuHanh
            modelBuilder.Entity<HeDieuHanh>()
                .HasKey(h => h.maHeDieuHanh);

            modelBuilder.Entity<HeDieuHanh>()
                .Property(h => h.tenHeDieuHanh).HasMaxLength(100);

            modelBuilder.Entity<HeDieuHanh>()
                .Property(h => h.trangThai);

            //--------------------------------------------------------------------------------

            // Cấu hình bảng DungLuongRom
            modelBuilder.Entity<DungLuongRom>()
                .HasKey(d => d.maDungLuongRom);

            modelBuilder.Entity<DungLuongRom>()
                .Property(d => d.kichThuocRom);

            modelBuilder.Entity<DungLuongRom>()
                .Property(d => d.trangThai);

            //--------------------------------------------------------------------------------

            // Cấu hình bảng DungLuongRam
            modelBuilder.Entity<DungLuongRam>()
                .HasKey(d => d.maDungLuongRam);

            modelBuilder.Entity<DungLuongRam>()
                .Property(d => d.kichThuocRam);

            modelBuilder.Entity<DungLuongRam>()
                .Property(d => d.trangThai);

            //--------------------------------------------------------------------------------

            // Cấu hình bảng CTSanPham
            modelBuilder.Entity<CTSanPham>()
                .HasKey(c => c.maImei);

            modelBuilder.Entity<CTSanPham>()
                .Property(c => c.maPhienBanSanPham);

            modelBuilder.Entity<CTSanPham>()
                .Property(c => c.maPhieuNhap);

            modelBuilder.Entity<CTSanPham>()
                .Property(c => c.maPhieuXuat);

            modelBuilder.Entity<CTSanPham>()
                .Property(c => c.tinhTrang);

            modelBuilder.Entity<CTSanPham>()
                .HasOne(c => c.PhienBanSanPham)
                .WithMany(p => p.CTSanPhams)
                .HasForeignKey(c => c.maPhienBanSanPham);
        }
    }
}
