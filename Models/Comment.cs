using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Comment
{
    public int MaComment { get; set; }

    public string NdbinhLuan { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string MaDienThoai { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public virtual DienThoai MaDienThoaiNavigation { get; set; } = null!;

    public virtual UserLogin UserNameNavigation { get; set; } = null!;
}
