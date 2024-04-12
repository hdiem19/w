using System;
using System.Collections.Generic;

namespace admin.Data;

public partial class DonHang
{
    public int IdDh { get; set; }

    public DateTime Tglap { get; set; }

    public DateTime Tggiao { get; set; }

    public string TongTien { get; set; } = null!;

    public string ThanhTien { get; set; } = null!;

    public string DiaChiGiaoHang { get; set; } = null!;

    public bool TrangThaiDon { get; set; }

    public int Id { get; set; }

    public virtual ICollection<CtdonHang> CtdonHangs { get; set; } = new List<CtdonHang>();

    public virtual TaiKhoan IdNavigation { get; set; } = null!;
}
