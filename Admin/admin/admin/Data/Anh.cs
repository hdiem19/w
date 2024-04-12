using System;
using System.Collections.Generic;

namespace admin.Data;

public partial class Anh
{
    public int IdAnh { get; set; }

    public string Url { get; set; } = null!;

    public int IdSp { get; set; }

    public virtual SanPham IdSpNavigation { get; set; } = null!;
}
