using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanHeHTQT.API;
using PhanHeHTQT.Models;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbToChucHopTacQuocTesController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbToChucHopTacQuocTesController(ApiServices services)
        {
            ApiServices_ = services;
        }

        // GET: TbToChucHopTacQuocTes
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TbToChucHopTacQuocTe> getall = await ApiServices_.GetAll<IdQuocGiaNavigation>("/api/htqt/ToChucHopTacQuocTe");
                // Lấy data từ các table khác có liên quan (khóa ngoài) để hiển thị trên Index
                return View(getall);

                // Bắt lỗi các trường hợp ngoại lệ
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbToChucHopTacQuocTes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacQuocTe = await _context.TbToChucHopTacQuocTes
                .Include(t => t.IdQuocGiaNavigation)
                .FirstOrDefaultAsync(m => m.IdToChucHopTacQuocTe == id);
            if (tbToChucHopTacQuocTe == null)
            {
                return NotFound();
            }

            return View(tbToChucHopTacQuocTe);
        }

        // GET: TbToChucHopTacQuocTes/Create
        public IActionResult Create()
        {
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich");
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "TenHinhThuc"); // Adjust the fields as necessary



            return View();
        }

        // POST: TbToChucHopTacQuocTes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdToChucHopTacQuocTe,MaHopTac,TenToChuc,IdQuocGia,NoiDung,ThoiGianKyKet,KetQuaHopTac,LoaiToChuc")] TbToChucHopTacQuocTe tbToChucHopTacQuocTe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbToChucHopTacQuocTe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbToChucHopTacQuocTe.IdQuocGia);
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "TenHinhThuc");
            return View(tbToChucHopTacQuocTe);
        }

        // GET: TbToChucHopTacQuocTes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacQuocTe = await _context.TbToChucHopTacQuocTes.FindAsync(id);
            if (tbToChucHopTacQuocTe == null)
            {
                return NotFound();
            }
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbToChucHopTacQuocTe.IdQuocGia);
            return View(tbToChucHopTacQuocTe);
        }

        // POST: TbToChucHopTacQuocTes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdToChucHopTacQuocTe,MaHopTac,TenToChuc,IdQuocGia,NoiDung,ThoiGianKyKet,KetQuaHopTac,LoaiToChuc")] TbToChucHopTacQuocTe tbToChucHopTacQuocTe)
        {
            if (id != tbToChucHopTacQuocTe.IdToChucHopTacQuocTe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbToChucHopTacQuocTe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbToChucHopTacQuocTeExists(tbToChucHopTacQuocTe.IdToChucHopTacQuocTe))
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
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbToChucHopTacQuocTe.IdQuocGia);
            return View(tbToChucHopTacQuocTe);
        }

        // GET: TbToChucHopTacQuocTes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacQuocTe = await _context.TbToChucHopTacQuocTes
                .Include(t => t.IdQuocGiaNavigation)
                .FirstOrDefaultAsync(m => m.IdToChucHopTacQuocTe == id);
            if (tbToChucHopTacQuocTe == null)
            {
                return NotFound();
            }

            return View(tbToChucHopTacQuocTe);
        }

        // POST: TbToChucHopTacQuocTes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbToChucHopTacQuocTe = await _context.TbToChucHopTacQuocTes.FindAsync(id);
            if (tbToChucHopTacQuocTe != null)
            {
                _context.TbToChucHopTacQuocTes.Remove(tbToChucHopTacQuocTe);
            }
                await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        }

        private bool TbToChucHopTacQuocTeExists(int id)
        {
            return _context.TbToChucHopTacQuocTes.Any(e => e.IdToChucHopTacQuocTe == id);
        }
    }
}
