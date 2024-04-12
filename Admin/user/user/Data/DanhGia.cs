using System;
using System.Collections.Generic;

namespace user.Data;

public partial class DanhGia
{
    public int IdDg { get; set; }

    public string BinhLuan { get; set; } = null!;

    public string HinhAnh { get; set; } = null!;

    public int Id { get; set; }

    public int IdSp { get; set; }

    public virtual TaiKhoan IdNavigation { get; set; } = null!;

    public virtual SanPham IdSpNavigation { get; set; } = null!;
}
