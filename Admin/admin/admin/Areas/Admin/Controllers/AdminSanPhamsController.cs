using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using admin.Data;
using admin.Areas.Admin.Anh;
namespace admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSanPhamsController : Controller
    {
        private readonly WebcafeContext _context;

        public AdminSanPhamsController(WebcafeContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminSanPhams
        public async Task<IActionResult> Index()
        {
            var webcafeContext = _context.SanPhams.Include(s => s.IdLoaiSpNavigation);
            return View(await webcafeContext.ToListAsync());
        }

        // GET: Admin/AdminSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.IdLoaiSpNavigation)
              
                .FirstOrDefaultAsync(m => m.IdSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Create
        public IActionResult Create()
        {
            ViewData["IdLoaiSp"] = new SelectList(_context.LoaiSps, "IdLoaiSp", "TenLoaiSp");
         
            return View();
        }

        // POST: Admin/AdminSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSp,TenSp,Hinh,MoTa,Soluong,Gia,IdLoaiSp")] SanPham sanPham, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {

                if (formFile != null)
                {
                    sanPham.Hinh = await image.Uploadfile(formFile, @"sanPham");
                }

                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLoaiSp"] = new SelectList(_context.LoaiSps, "IdLoaiSp", "TenLoaiSp", sanPham.IdLoaiSp);
         
            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["IdLoaiSp"] = new SelectList(_context.LoaiSps, "IdLoaiSp", "TenLoaiSp", sanPham.IdLoaiSp);
    
            return View(sanPham);
        }

        // POST: Admin/AdminSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSp,TenSp,Hinh,MoTa,Soluong,Gia,IdLoaiSp")] SanPham sanPham)
        {
            if (id != sanPham.IdSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.IdSp))
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
            ViewData["IdLoaiSp"] = new SelectList(_context.LoaiSps, "IdLoaiSp", "TenLoaiSp", sanPham.IdLoaiSp);
           
            return View(sanPham);
        }

        // GET: Admin/AdminSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.IdLoaiSpNavigation)
     
                .FirstOrDefaultAsync(m => m.IdSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/AdminSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.IdSp == id);
        }
    }
}
