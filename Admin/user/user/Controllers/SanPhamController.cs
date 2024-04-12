using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using user.Data;
using user.ViewModels;

namespace WebCF.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly WebcafeContext db;
        public SanPhamController (WebcafeContext context)
        {
            db = context;
        }
        public IActionResult Index( int? loai)
        {
            var sanPhams = db.SanPhams.AsQueryable();
            if (loai.HasValue)
            {
                sanPhams = sanPhams.Where(p => p.IdLoaiSp == loai.Value);
            }
            var result = sanPhams.Select(p => new SanPhamVM
            {
                MaSP = p.IdSp,
                TenSP = p.TenSp,
                DonGia = p.Gia ,
                Hinh = p.Hinh ?? "",
                MoTa = p.MoTa ?? "",
            });
            return View(result);
        }
        public IActionResult Search (string? query)
        {
            var sanPhams = db.SanPhams.AsQueryable();
            if (query != null)
            {
                sanPhams = sanPhams.Where(p => p.TenSp.Contains(query));
            }
            var result = sanPhams.Select(p => new SanPhamVM
            {
				MaSP = p.IdSp,
				TenSP = p.TenSp,
				DonGia = p.Gia,
				Hinh = p.Hinh ?? "",
				MoTa = p.MoTa ?? "",
				TenLoai = p.IdLoaiSpNavigation.TenLoaiSp
			});
            return View(result);
        }

        public IActionResult Detail(int id )
        {
            var data = db.SanPhams
                .Include(p => p.IdLoaiSpNavigation)
                .SingleOrDefault(p => p.IdSp == id);
            if (data == null)
            {
                
                return NotFound();
            }
            var result = new ChiTietSPVM
            {
                MaSP = data.IdSp,
                TenSP = data.TenSp,
                DonGia = data.Gia,

                Hinh = data.Hinh ?? string.Empty,
                MoTa = data.MoTa ?? string.Empty,
                TenLoai = data.IdLoaiSpNavigation.TenLoaiSp,
				
			};
             return View(result);
        }
    }
}
