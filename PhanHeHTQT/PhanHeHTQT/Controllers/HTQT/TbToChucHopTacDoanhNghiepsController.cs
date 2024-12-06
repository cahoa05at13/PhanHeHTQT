using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanHeHTQT.Models;
using PhanHeHTQT.Models.DM;
using PhanHeHTQT.API;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbToChucHopTacDoanhNghiepsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbToChucHopTacDoanhNghiepsController(ApiServices services)
        {
            ApiServices_ = services;
        }
        private async Task<List<TbToChucHopTacDoanhNghiep>> TbToChucHopTacDoanhNghieps()
        {
            List<TbToChucHopTacDoanhNghiep> tbToChucHopTacDoanhNghieps = await ApiServices_.GetAll<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep");
            List<DmLoaiDeAnChuongTrinh> dmLoaiDeAnChuongTrinhs = await ApiServices_.GetAll<DmLoaiDeAnChuongTrinh>("/api/dm/LoaiDeAnChuongTrinh");
            tbToChucHopTacDoanhNghieps.ForEach(item =>
            {
                item.IdLoaiDeAnNavigation = dmLoaiDeAnChuongTrinhs.FirstOrDefault(x => x.IdLoaiDeAnChuongTrinh == item.IdLoaiDeAn);
            });
            return tbToChucHopTacDoanhNghieps;
        }
        // GET: TbToChucHopTacDoanhNghieps
        public async Task<IActionResult> Index()
        {
            List<TbToChucHopTacDoanhNghiep> getall = await TbToChucHopTacDoanhNghieps();
            return View(getall);
        }

        // GET: TbToChucHopTacDoanhNghieps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacDoanhNghieps = await TbToChucHopTacDoanhNghieps();
            var tbToChucHopTacDoanhNghiep = tbToChucHopTacDoanhNghieps.FirstOrDefault(m => m.IdToChucHopTacDoanhNghiep == id);

            if (tbToChucHopTacDoanhNghiep == null)
            {
                return NotFound();
            }

            return View(tbToChucHopTacDoanhNghiep);
        }

        // GET: TbToChucHopTacDoanhNghieps/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdLoaiDeAn"] = new SelectList(await ApiServices_.GetAll<DmLoaiDeAnChuongTrinh>("/api/dm/LoaiDeAnChuongTrinh"), "IdLoaiDeAnChuongTrinh", "LoaiDeAnChuongTrinh");
            return View();
        }

        // POST: TbToChucHopTacDoanhNghieps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdToChucHopTacDoanhNghiep,MaToChucHopTacDoanhNghiep,TenToChucHopTacDoanhNghiep,NoiDungHopTac,NgayKyKet,KetQuaHopTac,IdLoaiDeAn,GiaTriGiaoDichCuaThiTruong")] TbToChucHopTacDoanhNghiep tbToChucHopTacDoanhNghiep)
        {
            if (ModelState.IsValid)
            {
                await ApiServices_.Create<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep", tbToChucHopTacDoanhNghiep);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLoaiDeAn"] = new SelectList(await ApiServices_.GetAll<DmLoaiDeAnChuongTrinh>("/api/dm/LoaiDeAnChuongTrinh"), "IdLoaiDeAnChuongTrinh", "LoaiDeAnChuongTrinh", tbToChucHopTacDoanhNghiep.IdLoaiDeAn);
            return View(tbToChucHopTacDoanhNghiep);
        }

        // GET: TbToChucHopTacDoanhNghieps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacDoanhNghiep = await ApiServices_.GetId<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep", id ?? 0);
            if (tbToChucHopTacDoanhNghiep == null)
            {
                return NotFound();
            }
            ViewData["IdLoaiDeAn"] = new SelectList(await ApiServices_.GetAll<DmLoaiDeAnChuongTrinh>("/api/dm/LoaiDeAnChuongTrinh"), "IdLoaiDeAnChuongTrinh", "LoaiDeAnChuongTrinh", tbToChucHopTacDoanhNghiep.IdLoaiDeAn);
            return View(tbToChucHopTacDoanhNghiep);
        }

        // POST: TbToChucHopTacDoanhNghieps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdToChucHopTacDoanhNghiep,MaToChucHopTacDoanhNghiep,TenToChucHopTacDoanhNghiep,NoiDungHopTac,NgayKyKet,KetQuaHopTac,IdLoaiDeAn,GiaTriGiaoDichCuaThiTruong")] TbToChucHopTacDoanhNghiep tbToChucHopTacDoanhNghiep)
        {
            if (id != tbToChucHopTacDoanhNghiep.IdToChucHopTacDoanhNghiep)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ApiServices_.Update<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep", id, tbToChucHopTacDoanhNghiep);
                    if (id != tbToChucHopTacDoanhNghiep.IdToChucHopTacDoanhNghiep)
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (await TbToChucHopTacDoanhNghiepExists(tbToChucHopTacDoanhNghiep.IdToChucHopTacDoanhNghiep) == false)
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
            ViewData["IdLoaiDeAn"] = new SelectList(await ApiServices_.GetAll<DmLoaiDeAnChuongTrinh>("/api/dm/LoaiDeAnChuongTrinh"), "IdLoaiDeAnChuongTrinh", "LoaiDeAnChuongTrinh", tbToChucHopTacDoanhNghiep.IdLoaiDeAn);
            return View(tbToChucHopTacDoanhNghiep);
        }

        // GET: TbToChucHopTacDoanhNghieps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacDoanhNghieps = await ApiServices_.GetAll<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep");
            var tbToChucHopTacDoanhNghiep = tbToChucHopTacDoanhNghieps.FirstOrDefault(m => m.IdToChucHopTacDoanhNghiep == id);
            if (tbToChucHopTacDoanhNghiep == null)
            {
                return NotFound();
            }

            return View(tbToChucHopTacDoanhNghiep);
        }

        // POST: TbToChucHopTacDoanhNghieps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ApiServices_.Delete<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbToChucHopTacDoanhNghiepExists(int id)
        {
            var tbToChucHopTacDoanhNghieps = await ApiServices_.GetAll<TbToChucHopTacDoanhNghiep>("/api/htqt/ToChucHopTacDoanhNghiep");
            return tbToChucHopTacDoanhNghieps.Any(e => e.IdToChucHopTacDoanhNghiep == id);
        }
    }
}
