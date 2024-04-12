using Microsoft.AspNetCore.Mvc;
using user.Data;
using user.ViewModels;

namespace user.ViewComponents
{

    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly WebcafeContext db;
        public MenuLoaiViewComponent(WebcafeContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.LoaiSps.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.IdLoaiSp,
                TenLoai = lo.TenLoaiSp,

            }).OrderBy(p => p.TenLoai);

            return View(data);
        }
    }
}

