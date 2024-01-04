using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class UserLogin
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte? UserRole { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
