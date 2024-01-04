using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class AnhSp
{
    public string MaDienThoai { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public short? ViTri { get; set; }

    public virtual DienThoai MaDienThoaiNavigation { get; set; } = null!;
}
