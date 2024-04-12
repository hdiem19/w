using System;
using System.Collections.Generic;

namespace user.Data;

public partial class LoaiSp
{
    public int IdLoaiSp { get; set; }

    public string TenLoaiSp { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
