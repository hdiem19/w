using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using admin.Data;

namespace admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoaiSpsController : Controller
    {
        private readonly WebcafeContext _context;

        public AdminLoaiSpsController(WebcafeContext context)
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
			return View(await _context.LoaiSps.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("IdLoaiSp,TenLoaiSp")] LoaiSp loaiSp)
		{
			if (ModelState.IsValid)
			{
				_context.Add(loaiSp);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(loaiSp);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var loaiSp = await _context.LoaiSps.FindAsync(id);
			if (loaiSp == null)
			{
				return NotFound();
			}
			return View(loaiSp);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("IdLoaiSp,TenLoaiSp")] LoaiSp loaiSp)
		{
			if (id != loaiSp.IdLoaiSp)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(loaiSp);
					await _context.SaveChangesAsync();

				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LoaiTKExists((int)loaiSp.IdLoaiSp))
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
			return View(loaiSp);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var loaiSp = await _context.LoaiSps.FirstOrDefaultAsync(m => m.IdLoaiSp == id);
			if (loaiSp == null)
			{
				return NotFound();
			}
			return View(loaiSp);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var loaiSp = await _context.LoaiSps.FindAsync(id);
			if (loaiSp != null)
			{
				_context.LoaiSps.Remove(loaiSp);
				await _context.SaveChangesAsync();

			}
			else
			{

			}
			return RedirectToAction(nameof(Index));
		}

		private bool LoaiTKExists(int id)
		{
			return _context.LoaiSps.Any(e => e.IdLoaiSp == id);
		}
	}
}
