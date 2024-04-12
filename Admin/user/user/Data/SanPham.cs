using System;
using System.Collections.Generic;

namespace user.Data;

public partial class SanPham
{
    public int IdSp { get; set; }

    public int? IdKm { get; set; }

    public string? KhuyenMaiIdKm { get; set; }

    public string TenSp { get; set; } = null!;

    public string? Hinh { get; set; }

    public string MoTa { get; set; } = null!;

    public string Soluong { get; set; } = null!;

    public double Gia { get; set; }

    public int IdLoaiSp { get; set; }

    public virtual ICollection<CtdonHang> CtdonHangs { get; set; } = new List<CtdonHang>();

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual LoaiSp IdLoaiSpNavigation { get; set; } = null!;

    public virtual KhuyenMai? KhuyenMaiIdKmNavigation { get; set; }
}
