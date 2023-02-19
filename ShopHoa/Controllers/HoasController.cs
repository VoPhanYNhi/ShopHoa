using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopHoa.Data;
using ShopHoa.Models;


namespace ShopHoa.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hoas
        public async Task<IActionResult> Index(string SearchString)
        {
            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa).ToListAsync();
            IQueryable<Hoa> hoaIQ = from s in _context.Hoa
                                    select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                hoaIQ = hoaIQ.Where(s => s.TenHoa.Contains(SearchString));
            }
            hoa = await hoaIQ.AsNoTracking().Include(h => h.LoaiHoa).ToListAsync();
            
            return View(hoa);
        }

        // GET: Hoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa)
                .FirstOrDefaultAsync(m => m.HoaId == id);
            if (hoa == null)
            {
                return NotFound();
            }

            return View(hoa);
        }

        // GET: Hoas/Create
        public IActionResult Create()
        {
            ViewData["LoaiRefId"] = new SelectList(_context.Set<LoaiHoa>(), "LoaiId", "TenLoai");
            return View();
        }

        // POST: Hoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("HoaId,TenHoa,Gia,SoLuong,MoTa,Image,LoaiRefId")] Hoa hoa)
        {
            if (ModelState.IsValid)
            {
                hoa.Image = Upload(file);
                _context.Add(hoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiRefId"] = new SelectList(_context.Set<LoaiHoa>(), "LoaiId", "TenLoai", hoa.LoaiRefId);
            return View(hoa);
        }

        // GET: Hoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa = await _context.Hoa.FindAsync(id);
            if (hoa == null)
            {
                return NotFound();
            }
            ViewData["LoaiRefId"] = new SelectList(_context.Set<LoaiHoa>(), "LoaiId", "TenLoai", hoa.LoaiRefId);
            return View(hoa);
        }

        // POST: Hoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("HoaId,TenHoa,Gia,SoLuong,MoTa,Image,LoaiRefId")] Hoa hoa)
        {
            if (id != hoa.HoaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        hoa.Image = Upload(file);
                    }
                    _context.Update(hoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaExists(hoa.HoaId))
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
            ViewData["LoaiRefId"] = new SelectList(_context.Set<LoaiHoa>(), "LoaiId", "TenLoai", hoa.LoaiRefId);
            return View(hoa);
        }

        // GET: Hoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoa = await _context.Hoa
                .Include(h => h.LoaiHoa)
                .FirstOrDefaultAsync(m => m.HoaId == id);
            if (hoa == null)
            {
                return NotFound();
            }

            return View(hoa);
        }

        // POST: Hoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoa = await _context.Hoa.FindAsync(id);
            _context.Hoa.Remove(hoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaExists(int id)
        {
            return _context.Hoa.Any(e => e.HoaId == id);
        }

        public string Upload(IFormFile file)
        {
            string fn = null;

            if (file != null)
            {
                // Phát sinh tên mới cho file để tránh trùng tên
                fn = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\{fn}"; // đường dẫn lưu file
                // upload file lên đường dẫn chỉ định
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return fn;
        }
    }
}
