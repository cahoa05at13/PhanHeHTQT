using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using PhanHeHTQT.API;
using PhanHeHTQT.Models;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbThoaThuanHopTacQuocTesController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbThoaThuanHopTacQuocTesController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbThoaThuanHopTacQuocTe>> TbThoaThuanHopTacQuocTes()
        {
            List<TbThoaThuanHopTacQuocTe> tbThoaThuanHopTacQuocTes = await ApiServices_.GetAll<TbThoaThuanHopTacQuocTe>("/api/htqt/ThoaThuanHopTacQuocTe");
            List<DmQuocTich> dmQuocTiches = await ApiServices_.GetAll<DmQuocTich>("/api/htqt/QuocTich");
            tbThoaThuanHopTacQuocTes.ForEach(item =>
            {
                item.IdQuocGiaNavigation = dmQuocTiches.FirstOrDefault(x => x.IdQuocTich == item.IdQuocGia);
            });
            return tbThoaThuanHopTacQuocTes;
        }

        // GET: TbThoaThuanHopTacQuocTes
        public async Task<IActionResult> Index()
        {
            List<TbThoaThuanHopTacQuocTe> getall = await TbThoaThuanHopTacQuocTes();
            return View(getall);
        }

        // GET: TbThoaThuanHopTacQuocTes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThoaThuanHopTacQuocTes = await TbThoaThuanHopTacQuocTes();
            var tbThoaThuanHopTacQuocTe =tbThoaThuanHopTacQuocTes.FirstOrDefault(m=> m.IdThoaThuanHopTacQuocTe == id);
            if (tbThoaThuanHopTacQuocTe == null)
            {
                return NotFound();
            }

            return View(tbThoaThuanHopTacQuocTe);
        }

        // GET: TbThoaThuanHopTacQuocTes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "TenNuoc");
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
                await ApiServices_.Create<TbThoaThuanHopTacQuocTe>("/api/htqt/TbThoaThuanHopTacQuocTe", tbThoaThuanHopTacQuocTe);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "TenNuoc", tbThoaThuanHopTacQuocTe.IdQuocGia);
            return View(tbThoaThuanHopTacQuocTe);
        }

        // GET: TbThoaThuanHopTacQuocTes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThoaThuanHopTacQuocTe = await ApiServices_.GetId<TbThoaThuanHopTacQuocTe>("/api/htqt/ThoaThuanHopTacQuocTe", id ?? 0);
            if (tbThoaThuanHopTacQuocTe == null)
            {
                return NotFound();
            }
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "TenNuoc", tbThoaThuanHopTacQuocTe.IdQuocGia);
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
                    await ApiServices_.Update<TbThoaThuanHopTacQuocTe>("/api/htqt/ThoaThuanHopTacQuocTe", id, tbThoaThuanHopTacQuocTe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TbThoaThuanHopTacQuocTeExists(tbThoaThuanHopTacQuocTe.IdThoaThuanHopTacQuocTe) == false)
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
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "TenNuoc", tbThoaThuanHopTacQuocTe.IdQuocGia);
            return View(tbThoaThuanHopTacQuocTe);
        }

        // GET: TbThoaThuanHopTacQuocTes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThoaThuanHopTacQuocTes = await ApiServices_.GetAll<TbThoaThuanHopTacQuocTe>("/api/htqt/ThoaThuanHopTacQuocTe");
            var tbThoaThuanHopTacQuocTe = tbThoaThuanHopTacQuocTes.FirstOrDefault(m => m.IdThoaThuanHopTacQuocTe == id);
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
            await ApiServices_.Delete<TbThoaThuanHopTacQuocTe>("/api/htqt/ThoaThuanHopTacQuocTe", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbThoaThuanHopTacQuocTeExists(int id)
        {
            var TbThoaThuanHopTacQuocTes = await ApiServices_.GetAll<TbThoaThuanHopTacQuocTe>("/api/htqt/ThoaThuanHopTacQuocTe");
            return TbThoaThuanHopTacQuocTes.Any(e => e.IdThoaThuanHopTacQuocTe == id);
        }
    }
}
