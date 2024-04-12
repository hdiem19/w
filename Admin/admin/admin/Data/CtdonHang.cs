using System;
using System.Collections.Generic;

namespace admin.Data;

public partial class CtdonHang
{
    public int IdCtdh { get; set; }

    public int IdDh { get; set; }

    public int IdSp { get; set; }

    public virtual DonHang IdDhNavigation { get; set; } = null!;

    public virtual SanPham IdSpNavigation { get; set; } = null!;
}
