using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HangSanXuat
{
    public string MaHsx { get; set; } = null!;

    public string TenHsx { get; set; } = null!;

    public virtual ICollection<DienThoai> DienThoais { get; set; } = new List<DienThoai>();
}
