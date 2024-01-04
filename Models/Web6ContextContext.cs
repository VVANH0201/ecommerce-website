using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTL.Models;

public partial class Web6ContextContext : DbContext
{
    public Web6ContextContext()
    {
    }

    public Web6ContextContext(DbContextOptions<Web6ContextContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSp> AnhSps { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietHdb> ChiTietHdbs { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DienThoai> DienThoais { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }

    public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HVO99UR\\VANH;Initial Catalog=Web6Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHdb>().ToTable(tb => tb.HasTrigger("CapNhatDGB"));
        modelBuilder.Entity<ChiTietHdb>().ToTable(tb => tb.HasTrigger("CapNhatSl"));
        modelBuilder.Entity<ChiTietHdb>().ToTable(tb => tb.HasTrigger("TongTienBan"));
        modelBuilder.Entity<ChiTietGioHang>().ToTable(tb => tb.HasTrigger("TongTienGH"));
        modelBuilder.Entity<AnhSp>(entity =>
        {
            entity.HasKey(e => new { e.MaDienThoai, e.TenFileAnh });

            entity.ToTable("AnhSP");

            entity.Property(e => e.MaDienThoai).HasMaxLength(25);
            entity.Property(e => e.TenFileAnh).HasMaxLength(100);

            entity.HasOne(d => d.MaDienThoaiNavigation).WithMany(p => p.AnhSps)
                .HasForeignKey(d => d.MaDienThoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnhSP");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => new { e.MaGioHang, e.MaDienThoai });

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.MaGioHang).HasMaxLength(25);
            entity.Property(e => e.MaDienThoai).HasMaxLength(25);

            entity.HasOne(d => d.MaDienThoaiNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaDienThoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietGioHang_MaDienThoai");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietGioHang_MaGioHang");
        });

        modelBuilder.Entity<ChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.MaHdb, e.MaDienThoai });

            entity.ToTable("ChiTietHDB", tb =>
                {
                    tb.HasTrigger("CapNhatDGB");
                    tb.HasTrigger("CapNhatSl");
                    tb.HasTrigger("TongTienBan");
                });

            entity.Property(e => e.MaHdb)
                .HasMaxLength(25)
                .HasColumnName("MaHDB");
            entity.Property(e => e.MaDienThoai).HasMaxLength(25);
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.MaDienThoaiNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaDienThoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_MaDienThoai");

            entity.HasOne(d => d.MaHdbNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_MaHDB");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu);

            entity.ToTable("ChucVu");

            entity.Property(e => e.MaChucVu).HasMaxLength(30);
            entity.Property(e => e.TenChucVu).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.MaComment).HasName("PK__Comment__36A7276A2A06A8A0");

            entity.ToTable("Comment");

            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.MaDienThoai).HasMaxLength(25);
            entity.Property(e => e.NdbinhLuan)
                .HasMaxLength(500)
                .HasColumnName("NDBinhLuan");
            entity.Property(e => e.UserName).HasMaxLength(40);

            entity.HasOne(d => d.MaDienThoaiNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MaDienThoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DienThoai");

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_UserName");
        });

        modelBuilder.Entity<DienThoai>(entity =>
        {
            entity.HasKey(e => e.MaDienThoai);

            entity.ToTable("DienThoai");

            entity.Property(e => e.MaDienThoai).HasMaxLength(25);
            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.GiaBan).HasColumnType("money");
            entity.Property(e => e.GiaNhap).HasColumnType("money");
            entity.Property(e => e.HeDieuHanh).HasMaxLength(30);
            entity.Property(e => e.KichThuoc).HasMaxLength(30);
            entity.Property(e => e.MaHsx)
                .HasMaxLength(15)
                .HasColumnName("MaHSX");
            entity.Property(e => e.ManHinh).HasMaxLength(30);
            entity.Property(e => e.Pin).HasMaxLength(15);
            entity.Property(e => e.Ram)
                .HasMaxLength(15)
                .HasColumnName("RAM");
            entity.Property(e => e.Rom)
                .HasMaxLength(15)
                .HasColumnName("ROM");
            entity.Property(e => e.TenDienThoai).HasMaxLength(150);
            entity.Property(e => e.TgbaoHanh).HasColumnName("TGBaoHanh");

            entity.HasOne(d => d.MaHsxNavigation).WithMany(p => p.DienThoais)
                .HasForeignKey(d => d.MaHsx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DienThoai_MaHSX");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang);

            entity.ToTable("GioHang");

            entity.Property(e => e.MaGioHang).HasMaxLength(25);
            entity.Property(e => e.TongTien).HasColumnType("money");
            entity.Property(e => e.UserName).HasMaxLength(40);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GioHang_UserName");
        });

        modelBuilder.Entity<HangSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaHsx);

            entity.ToTable("HangSanXuat");

            entity.Property(e => e.MaHsx)
                .HasMaxLength(15)
                .HasColumnName("MaHSX");
            entity.Property(e => e.TenHsx)
                .HasMaxLength(30)
                .HasColumnName("TenHSX");
        });

        modelBuilder.Entity<HoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHdb);

            entity.ToTable("HoaDonBan");

            entity.Property(e => e.MaHdb)
                .HasMaxLength(25)
                .HasColumnName("MaHDB");
            entity.Property(e => e.MaNhanVien).HasMaxLength(25);
            entity.Property(e => e.NgayBan).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("money");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK_HoaDonBan_MaKhachHang");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK_HoaDonBan_MaNhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E56257C2E5");

            entity.ToTable("KhachHang");

            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(40);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KhachHang_UserName");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien);

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien).HasMaxLength(25);
            entity.Property(e => e.AnhDaiDien).HasMaxLength(100);
            entity.Property(e => e.DiaChi).HasMaxLength(150);
            entity.Property(e => e.GioiTinh).HasMaxLength(15);
            entity.Property(e => e.MaChucVu).HasMaxLength(30);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(40);

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaChucVu)
                .HasConstraintName("FK_NhanVien_MaChucVu");

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_NhanVien_UserName");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.UserName);

            entity.ToTable("User_Login");

            entity.Property(e => e.UserName).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Password).HasMaxLength(40);
            entity.Property(e => e.UserRole).HasColumnName("User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
