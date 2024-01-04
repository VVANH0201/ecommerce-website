using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HoaDonBan
{
    public string MaHdb { get; set; } = null!;

    public DateTime NgayBan { get; set; }

    public int? MaKhachHang { get; set; }

    public string? MaNhanVien { get; set; }

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }
}
