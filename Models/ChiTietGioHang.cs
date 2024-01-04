using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChiTietGioHang
{
    public string MaGioHang { get; set; } = null!;

    public string MaDienThoai { get; set; } = null!;

    public int? SoLuong { get; set; }

    public virtual DienThoai MaDienThoaiNavigation { get; set; } = null!;

    public virtual GioHang MaGioHangNavigation { get; set; } = null!;
}
