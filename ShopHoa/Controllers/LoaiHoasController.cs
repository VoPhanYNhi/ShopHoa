using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopHoa.Data;
using ShopHoa.Models;

namespace ShopHoa.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoaiHoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiHoasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoaiHoas
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiHoa.ToListAsync());
        }

        // GET: LoaiHoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHoa = await _context.LoaiHoa
                .Include(h => h.Hoas)
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            if (loaiHoa == null)
            {
                return NotFound();
            }

            return View(loaiHoa);
        }

        // GET: LoaiHoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiId,TenLoai")] LoaiHoa loaiHoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiHoa);
        }

        // GET: LoaiHoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHoa = await _context.LoaiHoa.FindAsync(id);
            if (loaiHoa == null)
            {
                return NotFound();
            }
            return View(loaiHoa);
        }

        // POST: LoaiHoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiId,TenLoai")] LoaiHoa loaiHoa)
        {
            if (id != loaiHoa.LoaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiHoaExists(loaiHoa.LoaiId))
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
            return View(loaiHoa);
        }

        // GET: LoaiHoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHoa = await _context.LoaiHoa
                .FirstOrDefaultAsync(m => m.LoaiId == id);
            if (loaiHoa == null)
            {
                return NotFound();
            }

            return View(loaiHoa);
        }

        // POST: LoaiHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiHoa = await _context.LoaiHoa.FindAsync(id);
            _context.LoaiHoa.Remove(loaiHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiHoaExists(int id)
        {
            return _context.LoaiHoa.Any(e => e.LoaiId == id);
        }
    }
}
