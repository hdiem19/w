using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace user.Data;

public partial class WebcafeContext : DbContext
{
    public WebcafeContext()
    {
    }

    public WebcafeContext(DbContextOptions<WebcafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CtdonHang> CtdonHangs { get; set; }

    public virtual DbSet<DanhGia> DanhGias { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiSp> LoaiSps { get; set; }

    public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VPAF7UF\\SQLEXPRESS;Initial Catalog=Webcafe;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CtdonHang>(entity =>
        {
            entity.HasKey(e => e.IdCtdh);

            entity.ToTable("CTDonHangs");

            entity.HasIndex(e => e.IdDh, "IX_CTDonHangs_IdDH");

            entity.HasIndex(e => e.IdSp, "IX_CTDonHangs_IdSP");

            entity.Property(e => e.IdCtdh).HasColumnName("IdCTDH");
            entity.Property(e => e.IdDh).HasColumnName("IdDH");
            entity.Property(e => e.IdSp).HasColumnName("IdSP");

            entity.HasOne(d => d.IdDhNavigation).WithMany(p => p.CtdonHangs).HasForeignKey(d => d.IdDh);

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.CtdonHangs).HasForeignKey(d => d.IdSp);
        });

        modelBuilder.Entity<DanhGia>(entity =>
        {
            entity.HasKey(e => e.IdDg);

            entity.HasIndex(e => e.Id, "IX_DanhGias_Id");

            entity.HasIndex(e => e.IdSp, "IX_DanhGias_IdSP");

            entity.Property(e => e.IdDg).HasColumnName("IdDG");
            entity.Property(e => e.IdSp).HasColumnName("IdSP");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.DanhGia).HasForeignKey(d => d.Id);

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.DanhGia).HasForeignKey(d => d.IdSp);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.IdDh);

            entity.HasIndex(e => e.Id, "IX_DonHangs_Id");

            entity.Property(e => e.IdDh).HasColumnName("IdDH");
            entity.Property(e => e.Tggiao).HasColumnName("TGGiao");
            entity.Property(e => e.Tglap).HasColumnName("TGLap");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.DonHangs).HasForeignKey(d => d.Id);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.IdKm);

            entity.Property(e => e.IdKm).HasColumnName("IdKM");
            entity.Property(e => e.TenKm)
                .HasMaxLength(100)
                .HasColumnName("TenKM");
            entity.Property(e => e.TgbatDau).HasColumnName("TGBatDau");
            entity.Property(e => e.TgketThuc).HasColumnName("TGKetThuc");
        });

        modelBuilder.Entity<LoaiSp>(entity =>
        {
            entity.HasKey(e => e.IdLoaiSp);

            entity.ToTable("LoaiSPs");

            entity.Property(e => e.IdLoaiSp).HasColumnName("IdLoaiSP");
            entity.Property(e => e.TenLoaiSp)
                .HasMaxLength(100)
                .HasColumnName("TenLoaiSP");
        });

        modelBuilder.Entity<LoaiTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.IdLoaiTk);

            entity.Property(e => e.IdLoaiTk).HasColumnName("IdLoaiTK");
            entity.Property(e => e.LoaiTk)
                .HasMaxLength(100)
                .HasColumnName("LoaiTK");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.IdSp);

            entity.HasIndex(e => e.IdLoaiSp, "IX_SanPhams_IdLoaiSP");

            entity.HasIndex(e => e.KhuyenMaiIdKm, "IX_SanPhams_KhuyenMaiIdKM");

            entity.Property(e => e.IdSp).HasColumnName("IdSP");
            entity.Property(e => e.IdKm).HasColumnName("IdKM");
            entity.Property(e => e.IdLoaiSp).HasColumnName("IdLoaiSP");
            entity.Property(e => e.KhuyenMaiIdKm).HasColumnName("KhuyenMaiIdKM");
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.IdLoaiSpNavigation).WithMany(p => p.SanPhams).HasForeignKey(d => d.IdLoaiSp);

            entity.HasOne(d => d.KhuyenMaiIdKmNavigation).WithMany(p => p.SanPhams).HasForeignKey(d => d.KhuyenMaiIdKm);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasIndex(e => e.IdLoaiTk, "IX_TaiKhoans_IdLoaiTK");

            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.IdLoaiTk).HasColumnName("IdLoaiTK");
            entity.Property(e => e.MatKhau).HasMaxLength(20);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .HasColumnName("SDT");
            entity.Property(e => e.TenDn)
                .HasMaxLength(100)
                .HasColumnName("TenDN");

            entity.HasOne(d => d.IdLoaiTkNavigation).WithMany(p => p.TaiKhoans).HasForeignKey(d => d.IdLoaiTk);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
