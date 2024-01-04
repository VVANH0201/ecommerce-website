using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class GioHang
{
    public string MaGioHang { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual UserLogin UserNameNavigation { get; set; } = null!;
}
