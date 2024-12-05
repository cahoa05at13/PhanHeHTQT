using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanHeHTQT.Models;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbThongTinHopTacsController : Controller
    {
        private readonly HemisContext _context;

        public TbThongTinHopTacsController(HemisContext context)
        {
            _context = context;
        }

        // GET: TbThongTinHopTacs
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbThongTinHopTacs.Include(t => t.IdHinhThucHopTacNavigation).Include(t => t.IdToChucHopTacNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: TbThongTinHopTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await _context.TbThongTinHopTacs
                .Include(t => t.IdHinhThucHopTacNavigation)
                .Include(t => t.IdToChucHopTacNavigation)
                .FirstOrDefaultAsync(m => m.IdThongTinHopTac == id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }

            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Create
        public IActionResult Create()
        {
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac");
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe");
            return View();
        }

        // POST: TbThongTinHopTacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThongTinHopTac,IdToChucHopTac,ThoiGianHopTacTu,ThoiGianHopTacDen,TenThoaThuan,ThongTinLienHeDoiTac,MucTieu,DonViTrienKhai,IdHinhThucHopTac,SanPhamChinh")] TbThongTinHopTac tbThongTinHopTac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbThongTinHopTac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await _context.TbThongTinHopTacs.FindAsync(id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // POST: TbThongTinHopTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThongTinHopTac,IdToChucHopTac,ThoiGianHopTacTu,ThoiGianHopTacDen,TenThoaThuan,ThongTinLienHeDoiTac,MucTieu,DonViTrienKhai,IdHinhThucHopTac,SanPhamChinh")] TbThongTinHopTac tbThongTinHopTac)
        {
            if (id != tbThongTinHopTac.IdThongTinHopTac)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbThongTinHopTac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbThongTinHopTacExists(tbThongTinHopTac.IdThongTinHopTac))
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
            ViewData["IdHinhThucHopTac"] = new SelectList(_context.DmHinhThucHopTacs, "IdHinhThucHopTac", "IdHinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(_context.TbToChucHopTacQuocTes, "IdToChucHopTacQuocTe", "IdToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await _context.TbThongTinHopTacs
                .Include(t => t.IdHinhThucHopTacNavigation)
                .Include(t => t.IdToChucHopTacNavigation)
                .FirstOrDefaultAsync(m => m.IdThongTinHopTac == id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }

            return View(tbThongTinHopTac);
        }

        // POST: TbThongTinHopTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbThongTinHopTac = await _context.TbThongTinHopTacs.FindAsync(id);
            if (tbThongTinHopTac != null)
            {
                _context.TbThongTinHopTacs.Remove(tbThongTinHopTac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbThongTinHopTacExists(int id)
        {
            return _context.TbThongTinHopTacs.Any(e => e.IdThongTinHopTac == id);
        }
    }
}
