using System;
using System.Collections.Generic;

namespace admin.Data;

public partial class LoaiTaiKhoan
{
    public int IdLoaiTk { get; set; }

    public string LoaiTk { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
