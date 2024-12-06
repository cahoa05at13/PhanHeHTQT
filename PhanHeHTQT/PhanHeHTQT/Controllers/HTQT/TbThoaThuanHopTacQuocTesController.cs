﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanHeHTQT.Models;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbThoaThuanHopTacQuocTesController : Controller
    {
        private readonly HemisContext _context;

        public TbThoaThuanHopTacQuocTesController(HemisContext context)
        {
            _context = context;
        }

        // GET: TbThoaThuanHopTacQuocTes
        public async Task<IActionResult> Index()
        {
            var hemisContext = _context.TbThoaThuanHopTacQuocTes.Include(t => t.IdQuocGiaNavigation);
            return View(await hemisContext.ToListAsync());
        }

        // GET: TbThoaThuanHopTacQuocTes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThoaThuanHopTacQuocTe = await _context.TbThoaThuanHopTacQuocTes
                .Include(t => t.IdQuocGiaNavigation)
                .FirstOrDefaultAsync(m => m.IdThoaThuanHopTacQuocTe == id);
            if (tbThoaThuanHopTacQuocTe == null)
            {
                return NotFound();
            }

            return View(tbThoaThuanHopTacQuocTe);
        }

        // GET: TbThoaThuanHopTacQuocTes/Create
        public IActionResult Create()
        {
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich");
            return View();
        }

        // POST: TbThoaThuanHopTacQuocTes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThoaThuanHopTacQuocTe,MaThoaThuan,TenThoaThuan,NoiDungTomTat,TenToChuc,NgayKyKet,SoVanBanKyKet,IdQuocGia,NgayHetHan")] TbThoaThuanHopTacQuocTe tbThoaThuanHopTacQuocTe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbThoaThuanHopTacQuocTe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbThoaThuanHopTacQuocTe.IdQuocGia);
            return View(tbThoaThuanHopTacQuocTe);
        }

        // GET: TbThoaThuanHopTacQuocTes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThoaThuanHopTacQuocTe = await _context.TbThoaThuanHopTacQuocTes.FindAsync(id);
            if (tbThoaThuanHopTacQuocTe == null)
            {
                return NotFound();
            }
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbThoaThuanHopTacQuocTe.IdQuocGia);
            return View(tbThoaThuanHopTacQuocTe);
        }

        // POST: TbThoaThuanHopTacQuocTes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThoaThuanHopTacQuocTe,MaThoaThuan,TenThoaThuan,NoiDungTomTat,TenToChuc,NgayKyKet,SoVanBanKyKet,IdQuocGia,NgayHetHan")] TbThoaThuanHopTacQuocTe tbThoaThuanHopTacQuocTe)
        {
            if (id != tbThoaThuanHopTacQuocTe.IdThoaThuanHopTacQuocTe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbThoaThuanHopTacQuocTe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbThoaThuanHopTacQuocTeExists(tbThoaThuanHopTacQuocTe.IdThoaThuanHopTacQuocTe))
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
            ViewData["IdQuocGia"] = new SelectList(_context.DmQuocTiches, "IdQuocTich", "IdQuocTich", tbThoaThuanHopTacQuocTe.IdQuocGia);
            return View(tbThoaThuanHopTacQuocTe);
        }

        // GET: TbThoaThuanHopTacQuocTes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThoaThuanHopTacQuocTe = await _context.TbThoaThuanHopTacQuocTes
                .Include(t => t.IdQuocGiaNavigation)
                .FirstOrDefaultAsync(m => m.IdThoaThuanHopTacQuocTe == id);
            if (tbThoaThuanHopTacQuocTe == null)
            {
                return NotFound();
            }

            return View(tbThoaThuanHopTacQuocTe);
        }

        // POST: TbThoaThuanHopTacQuocTes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbThoaThuanHopTacQuocTe = await _context.TbThoaThuanHopTacQuocTes.FindAsync(id);
            if (tbThoaThuanHopTacQuocTe != null)
            {
                _context.TbThoaThuanHopTacQuocTes.Remove(tbThoaThuanHopTacQuocTe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbThoaThuanHopTacQuocTeExists(int id)
        {
            return _context.TbThoaThuanHopTacQuocTes.Any(e => e.IdThoaThuanHopTacQuocTe == id);
        }
    }
}
