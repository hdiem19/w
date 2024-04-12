using System;
using System.Collections.Generic;

namespace admin.Data;

public partial class KhuyenMai
{
    public string IdKm { get; set; } = null!;

    public string TenKm { get; set; } = null!;

    public DateTime TgbatDau { get; set; }

    public DateTime TgketThuc { get; set; }

    public string PhanTramGiam { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
