using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class DienThoai
{
    public string MaDienThoai { get; set; } = null!;

    public string? TenDienThoai { get; set; }

    public string Ram { get; set; } = null!;

    public string Rom { get; set; } = null!;

    public string Pin { get; set; } = null!;

    public string HeDieuHanh { get; set; } = null!;

    public string ManHinh { get; set; } = null!;

    public string KichThuoc { get; set; } = null!;

    public string? Anh { get; set; }

    public decimal? GiaBan { get; set; }

    public decimal? GiaNhap { get; set; }

    public int? SoLuong { get; set; }

    public int? TgbaoHanh { get; set; }

    public string MaHsx { get; set; } = null!;

    public virtual ICollection<AnhSp> AnhSps { get; set; } = new List<AnhSp>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual HangSanXuat MaHsxNavigation { get; set; } = null!;
}
