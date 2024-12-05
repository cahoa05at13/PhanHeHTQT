using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PhanHeHTQT.API;
using PhanHeHTQT.Models;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbHoiThaoHoiNghisController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbHoiThaoHoiNghisController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbHoiThaoHoiNghi>> TbHoiThaoHoiNghis()
        {
            List<TbHoiThaoHoiNghi> tbHoiThaoHoiNghis = await ApiServices_.GetAll<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi");
            List<DmNguonKinhPhi> dmNguonKinhPhis = await ApiServices_.GetAll<DmNguonKinhPhi>("/api/dm/NguonKinhPhi");
            tbHoiThaoHoiNghis.ForEach(item => { 
            item.IdNguonKinhPhiHoiThaoNavigation = dmNguonKinhPhis.FirstOrDefault(x => x.IdNguonKinhPhi == item.IdNguonKinhPhiHoiThao);
            });
            return tbHoiThaoHoiNghis;
        }
        // GET: TbHoiThaoHoiNghis
        public async Task<IActionResult> Index()
        {
            List<TbHoiThaoHoiNghi> getall = await TbHoiThaoHoiNghis();
            return View(getall);
        }

        // GET: TbHoiThaoHoiNghis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tbHoiThaoHoiNghis = await TbHoiThaoHoiNghis();
            var tbHoiThaoHoiNghi = tbHoiThaoHoiNghis.FirstOrDefault(m => m.IdHoiThaoHoiNghi == id);

           if (tbHoiThaoHoiNghi == null)
            {
                return NotFound();
            }

            return View(tbHoiThaoHoiNghi);
        }

        // GET: TbHoiThaoHoiNghis/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdNguonKinhPhiHoiThao"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhi>("/api/dm/NguonKinhPhi"), "IdNguonKinhPhi", "IdNguonKinhPhi");
            return View();
        }

        // POST: TbHoiThaoHoiNghis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHoiThaoHoiNghi,MaHoiThaoHoiNghi,TenHoiThaoHoiNghi,CoQuanCoThamQuyenCapPhep,MucTieu,NoiDung,SoLuongDaiBieuThamDu,SoLuongDaiBieuQuocTeThamDu,ThoiGianToChuc,DiaDiemToChuc,IdNguonKinhPhiHoiThao,DonViChuTri")] TbHoiThaoHoiNghi tbHoiThaoHoiNghi)
        {
            if (await TbHoiThaoHoiNghiExists(tbHoiThaoHoiNghi.IdHoiThaoHoiNghi)) ModelState.AddModelError("IdHoiThaoHoiNghi", "ID này đã tồn tại!");
            if (ModelState.IsValid)
            {
                await ApiServices_.Create<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi", tbHoiThaoHoiNghi);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNguonKinhPhiHoiThao"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhi>("/api/htqt/NguonKinhPhi"), "IdNguonKinhPhi", "IdNguonKinhPhi", tbHoiThaoHoiNghi.IdNguonKinhPhiHoiThao);
            return View(tbHoiThaoHoiNghi);
        }

        // GET: TbHoiThaoHoiNghis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbHoiThaoHoiNghi = await ApiServices_.GetId<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi", id ?? 0);
            if (tbHoiThaoHoiNghi == null)
            {
                return NotFound();
            }
            ViewData["IdNguonKinhPhiHoiThao"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhi>("/api/htqt/NguonKinhPhi"), "IdNguonKinhPhi", "IdNguonKinhPhi", tbHoiThaoHoiNghi.IdNguonKinhPhiHoiThao);
            return View(tbHoiThaoHoiNghi);
        }

        // POST: TbHoiThaoHoiNghis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHoiThaoHoiNghi,MaHoiThaoHoiNghi,TenHoiThaoHoiNghi,CoQuanCoThamQuyenCapPhep,MucTieu,NoiDung,SoLuongDaiBieuThamDu,SoLuongDaiBieuQuocTeThamDu,ThoiGianToChuc,DiaDiemToChuc,IdNguonKinhPhiHoiThao,DonViChuTri")] TbHoiThaoHoiNghi tbHoiThaoHoiNghi)
        {
            if (id != tbHoiThaoHoiNghi.IdHoiThaoHoiNghi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ApiServices_.Update<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi", id, tbHoiThaoHoiNghi);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TbHoiThaoHoiNghiExists(tbHoiThaoHoiNghi.IdHoiThaoHoiNghi) == false)
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
            ViewData["IdNguonKinhPhiHoiThao"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhi>("/api/htqt/NguonKinhPhi"), "IdNguonKinhPhi", "IdNguonKinhPhi", tbHoiThaoHoiNghi.IdNguonKinhPhiHoiThao);
            return View(tbHoiThaoHoiNghi);
        }

        // GET: TbHoiThaoHoiNghis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbHoiThaoHoiNghis = await ApiServices_.GetAll<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi");
            var tbHoiThaoHoiNghi = tbHoiThaoHoiNghis.FirstOrDefault(m => m.IdHoiThaoHoiNghi == id);
            if (tbHoiThaoHoiNghi == null)
            {
                return NotFound();
            }

            return View(tbHoiThaoHoiNghi);
        }

        // POST: TbHoiThaoHoiNghis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ApiServices_.Delete<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbHoiThaoHoiNghiExists(int id)
        {
            var tbHoiThaoHoiNghis = await ApiServices_.GetAll<TbHoiThaoHoiNghi>("/api/htqt/HoiThaoHoiNghi");
            return tbHoiThaoHoiNghis.Any(e => e.IdHoiThaoHoiNghi == id);
        }
    }
}
