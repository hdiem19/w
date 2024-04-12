using System;
using System.Collections.Generic;

namespace user.Data;

public partial class TaiKhoan
{
    public int Id { get; set; }

    public string TenDn { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public int IdLoaiTk { get; set; }

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual LoaiTaiKhoan IdLoaiTkNavigation { get; set; } = null!;
}
