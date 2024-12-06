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
    public class TbThanhPhanThamGiaDoanCongTacsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbThanhPhanThamGiaDoanCongTacsController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbThanhPhanThamGiaDoanCongTac>> TbThanhPhanThamGiaDoanCongTacs()
        {
            List<TbThanhPhanThamGiaDoanCongTac> tbThanhPhanThamGiaDoanCongTacs = await ApiServices_.GetAll<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongTac");
            List<TbCanBo> tbCanBos = await ApiServices_.GetAll<TbCanBo>("/api/cb/CanBo");
            List<TbDoanCongTac> tbDoanCongTacs = await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac");
            List<DmVaiTroThamGium> dmVaiTroThamGias = await ApiServices_.GetAll<DmVaiTroThamGium>("/api/dm/VaiTroThamGia");
            tbThanhPhanThamGiaDoanCongTacs.ForEach(item =>
            {
                item.IdCanBoNavigation = tbCanBos.FirstOrDefault(x => x.IdCanBo == item.IdCanBo);
                item.IdDoanCongTacNavigation = tbDoanCongTacs.FirstOrDefault(x => x.IdDoanCongTac == item.IdDoanCongTac);
                item.IdVaiTroThamGiaNavigation = dmVaiTroThamGias.FirstOrDefault(x => x.IdVaiTroThamGia == item.IdVaiTroThamGia);
            });
            return tbThanhPhanThamGiaDoanCongTacs;
        }

        // GET: TbThanhPhanThamGiaDoanCongTacs
        public async Task<IActionResult> Index()
        {
            List<TbThanhPhanThamGiaDoanCongTac> getall = await TbThanhPhanThamGiaDoanCongTacs();
            return View(getall);
        }

        // GET: TbThanhPhanThamGiaDoanCongTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThanhPhanThamGiaDoanCongTacs = await TbThanhPhanThamGiaDoanCongTacs();
            var tbThanhPhanThamGiaDoanCongTac = tbThanhPhanThamGiaDoanCongTacs.FirstOrDefault(m => m.IdThanhPhanThamGiaDoanCongTac == id);
            if (tbThanhPhanThamGiaDoanCongTac == null)
            {
                return NotFound();
            }

            return View(tbThanhPhanThamGiaDoanCongTac);
        }

        // GET: TbThanhPhanThamGiaDoanCongTacs/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/Canbo"), "IdCanBo", "CanBo");
            ViewData["IdDoanCongTac"] = new SelectList(await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac"), "IdDoanCongTac", "DoanCongTac");
            ViewData["IdVaiTroThamGia"] = new SelectList(await ApiServices_.GetAll<DmVaiTroThamGium>("/api/dm/VaiTroThamGia"), "IdVaiTroThamGia", "VaiTroThamGia");
            return View();
        }

        // POST: TbThanhPhanThamGiaDoanCongTacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdThanhPhanThamGiaDoanCongTac,IdDoanCongTac,IdCanBo,IdVaiTroThamGia")] TbThanhPhanThamGiaDoanCongTac tbThanhPhanThamGiaDoanCongTac)
        {
            if (ModelState.IsValid)
            {
                await ApiServices_.Create<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongTac", tbThanhPhanThamGiaDoanCongTac);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/Canbo"), "IdCanBo", "CanBo", tbThanhPhanThamGiaDoanCongTac.IdCanBo);
            ViewData["IdDoanCongTac"] = new SelectList(await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac"), "IdDoanCongTac", "DoanCongTac", tbThanhPhanThamGiaDoanCongTac.IdDoanCongTac);
            ViewData["IdVaiTroThamGia"] = new SelectList(await ApiServices_.GetAll<DmVaiTroThamGium>("/api/dm/VaiTroThamGia"), "IdVaiTroThamGia", "VaiTroThamGia", tbThanhPhanThamGiaDoanCongTac.IdVaiTroThamGia);

            return View(tbThanhPhanThamGiaDoanCongTac);
        }

        // GET: TbThanhPhanThamGiaDoanCongTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThanhPhanThamGiaDoanCongTac = await ApiServices_.GetId<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongtac", id ?? 0);
            if (tbThanhPhanThamGiaDoanCongTac == null)
            {
                return NotFound();
            }
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/Canbo"), "IdCanBo", "CanBo", tbThanhPhanThamGiaDoanCongTac.IdCanBo);
            ViewData["IdDoanCongTac"] = new SelectList(await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac"), "IdDoanCongTac", "DoanCongTac", tbThanhPhanThamGiaDoanCongTac.IdDoanCongTac);
            ViewData["IdVaiTroThamGia"] = new SelectList(await ApiServices_.GetAll<DmVaiTroThamGium>("/api/dm/VaiTroThamGia"), "IdVaiTroThamGia", "VaiTroThamGia", tbThanhPhanThamGiaDoanCongTac.IdVaiTroThamGia);
            return View(tbThanhPhanThamGiaDoanCongTac);
        }

        // POST: TbThanhPhanThamGiaDoanCongTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdThanhPhanThamGiaDoanCongTac,IdDoanCongTac,IdCanBo,IdVaiTroThamGia")] TbThanhPhanThamGiaDoanCongTac tbThanhPhanThamGiaDoanCongTac)
        {
            if (id != tbThanhPhanThamGiaDoanCongTac.IdThanhPhanThamGiaDoanCongTac)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ApiServices_.Update<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongTac", id, tbThanhPhanThamGiaDoanCongTac);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TbThanhPhanThamGiaDoanCongTacExists(tbThanhPhanThamGiaDoanCongTac.IdThanhPhanThamGiaDoanCongTac) == false)
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
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/Canbo"), "IdCanBo", "CanBo", tbThanhPhanThamGiaDoanCongTac.IdCanBo);
            ViewData["IdDoanCongTac"] = new SelectList(await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac"), "IdDoanCongTac", "DoanCongTac", tbThanhPhanThamGiaDoanCongTac.IdDoanCongTac);
            ViewData["IdVaiTroThamGia"] = new SelectList(await ApiServices_.GetAll<DmVaiTroThamGium>("/api/dm/VaiTroThamGia"), "IdVaiTroThamGia", "VaiTroThamGia", tbThanhPhanThamGiaDoanCongTac.IdVaiTroThamGia);
            return View(tbThanhPhanThamGiaDoanCongTac);
        }

        // GET: TbThanhPhanThamGiaDoanCongTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThanhPhanThamGiaDoanCongTacs = await ApiServices_.GetAll<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongTac");
            var tbThanhPhanThamGiaDoanCongTac = tbThanhPhanThamGiaDoanCongTacs.FirstOrDefault(m => m.IdThanhPhanThamGiaDoanCongTac == id);
            if (tbThanhPhanThamGiaDoanCongTac == null)
            {
                return NotFound();
            }

            return View(tbThanhPhanThamGiaDoanCongTac);
        }

        // POST: TbThanhPhanThamGiaDoanCongTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ApiServices_.Delete<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongTac", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbThanhPhanThamGiaDoanCongTacExists(int id)
        {
            var tbThanhPhanThamGiaDoanCongTacs = await ApiServices_.GetAll<TbThanhPhanThamGiaDoanCongTac>("/api/htqt/ThanhPhanThamGiaDoanCongTac");
            return tbThanhPhanThamGiaDoanCongTacs.Any(e => e.IdThanhPhanThamGiaDoanCongTac == id);
        }
    }
}
