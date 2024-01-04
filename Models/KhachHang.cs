using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string UserName { get; set; } = null!;

    public string? TenKhachHang { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();

    public virtual UserLogin UserNameNavigation { get; set; } = null!;
}
