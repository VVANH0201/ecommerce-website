using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChiTietHdb
{
    public string MaHdb { get; set; } = null!;

    public string MaDienThoai { get; set; } = null!;

    public int? SoLuong { get; set; }

    public decimal? DonGiaBan { get; set; }

    public double? GiamGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual DienThoai MaDienThoaiNavigation { get; set; } = null!;

    public virtual HoaDonBan MaHdbNavigation { get; set; } = null!;
}
