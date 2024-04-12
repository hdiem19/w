using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using admin.Data;
using static System.Net.Mime.MediaTypeNames;

namespace admin.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminLoaiTaiKhoansController : Controller
	{
		private readonly WebcafeContext _context;

		public AdminLoaiTaiKhoansController(WebcafeContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			if (TempData.ContainsKey("SuccessMessage"))
			{
				ViewBag.SuccessMessage = TempData["SuccessMessage"];
				TempData["ShowSuccessMessage"] = true;
			}
			return View(await _context.LoaiTaiKhoans.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("IdLoaiTk,LoaiTk")] LoaiTaiKhoan loaiTaiKhoan)
		{
			if (ModelState.IsValid)
			{
				_context.Add(loaiTaiKhoan);
				await _context.SaveChangesAsync();
	
				return RedirectToAction(nameof(Index));
			}
			return View(loaiTaiKhoan);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var loaiTaiKhoan = await _context.LoaiTaiKhoans.FindAsync(id);
			if (loaiTaiKhoan == null)
			{
				return NotFound();
			}
			return View(loaiTaiKhoan);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("IdLoaiTk,LoaiTk")] LoaiTaiKhoan loaiTaiKhoan)
		{
			if (id != loaiTaiKhoan.IdLoaiTk)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(loaiTaiKhoan);
					await _context.SaveChangesAsync();
			
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LoaiTKExists((int)loaiTaiKhoan.IdLoaiTk))
					{
				
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(loaiTaiKhoan);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var loaiTaiKhoan = await _context.LoaiTaiKhoans.FirstOrDefaultAsync(m => m.IdLoaiTk == id);
			if (loaiTaiKhoan == null)
			{
				return NotFound();
			}
			return View(loaiTaiKhoan);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var loaiTaiKhoan = await _context.LoaiTaiKhoans.FindAsync(id);
			if (loaiTaiKhoan != null)
			{
				_context.LoaiTaiKhoans.Remove(loaiTaiKhoan);
				await _context.SaveChangesAsync();
				
			}
			else
			{
				
			}
			return RedirectToAction(nameof(Index));
		}

		private bool LoaiTKExists(int id)
		{
			return _context.LoaiTaiKhoans.Any(e => e.IdLoaiTk == id);
		}
	}
}
