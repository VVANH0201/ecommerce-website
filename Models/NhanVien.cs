using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string? UserName { get; set; }

    public string? TenNhanVien { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? MaChucVu { get; set; }

    public string? AnhDaiDien { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();

    public virtual ChucVu? MaChucVuNavigation { get; set; }

    public virtual UserLogin? UserNameNavigation { get; set; }
}
