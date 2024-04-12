using AutoMapper;
using user.Data;
using user.Helpers;
using user.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace user.Controllers
{
	public class TaiKhoanController : Controller
	{
		private readonly WebcafeContext db;
		private readonly IMapper _mapper;

		public TaiKhoanController(WebcafeContext context, IMapper mapper)
		{
			db = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}
		
		[HttpPost]
		public IActionResult DangKy(RegisterVM model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var taikhoan = _mapper.Map<TaiKhoan>(model);
					taikhoan.MatKhau = model.MatKhau;
                    taikhoan.IdLoaiTk = 8;
					taikhoan.Sdt = model.DienThoai;
					taikhoan.TenDn = model.MaKh;
					taikhoan.HoTen = model.HoTen;
					taikhoan.Email = model.Email;
					taikhoan.DiaChi= model.DiaChi;
                    db.Add(taikhoan);
					db.SaveChanges();
					return RedirectToAction("Index", "Home");
				}
				catch (Exception ex)
				{
					var mess = $"{ex.Message} shh";
				}
			}
			return View();
		}
		

		[HttpGet]
		public IActionResult DangNhap(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid)
			{
				var taikhoan = db.TaiKhoans.SingleOrDefault(kh => kh.Id.ToString() == model.LoginName);
				if (taikhoan == null)
				{
					ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
				}
				else
				{
					if(taikhoan.MatKhau != model.Password)
					{
						ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
					}
					else
					{
						var claims = new List<Claim> { new Claim(ClaimTypes.Email, taikhoan.Email),
						               new Claim(ClaimTypes.Name, taikhoan.HoTen)
						};
					
					     var claimsIdentity = new ClaimsIdentity(claims,"login");
						var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

						await HttpContext.SignInAsync(claimsPrincipal);
						if (Url.IsLocalUrl(ReturnUrl))
						{
							return Redirect(ReturnUrl);
						}
						else
						{
							return Redirect("/");
						}
					} 					   
				}
			}
			return View();
		}
		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}
		[Authorize]
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}

}
