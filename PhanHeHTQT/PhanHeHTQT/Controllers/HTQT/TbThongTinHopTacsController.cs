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
    public class TbThongTinHopTacsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbThongTinHopTacsController(ApiServices services)
        {
            ApiServices_ = services;
        }
        private async Task<List<TbThongTinHopTac>> TbThongTinHopTacs()
        {
            List<TbThongTinHopTac> tbThongTinHopTacs = await ApiServices_.GetAll<TbThongTinHopTac>("/api/htqt/thongtinhoptac");
            List<DmHinhThucHopTac> dmHinhThucHopTacs = await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac");
            List<TbToChucHopTacQuocTe> tbToChucHopTacQuocTes = await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe");
            tbThongTinHopTacs.ForEach(item =>
            {
                item.IdHinhThucHopTacNavigation = dmHinhThucHopTacs.FirstOrDefault(x => x.IdHinhThucHopTac == item.IdHinhThucHopTac);
                item.IdToChucHopTacNavigation = tbToChucHopTacQuocTes.FirstOrDefault(x => x.IdToChucHopTacQuocTe == item.IdToChucHopTac);
            });
            return tbThongTinHopTacs;
        }
        // GET: TbThongTinHopTacs
        public async Task<IActionResult> Index()
        {
            List<TbThongTinHopTac> getall = await TbThongTinHopTacs();
            return View(getall);
        }

        // GET: TbThongTinHopTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTacs = await TbThongTinHopTacs();
            var tbThongTinHopTac = tbThongTinHopTacs.FirstOrDefault(m => m.IdThongTinHopTac == id);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }

            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdHinhThucHopTac"] = new SelectList(await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac"), "IdHinhThucHopTac", "HinhThucHopTac");
            ViewData["IdToChucHopTac"] = new SelectList(await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe"), "IdToChucHopTacQuocTe", "ToChucHopTacQuocTe");
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
                await ApiServices_.Create<TbThongTinHopTac>("/api/htqt/ThongTinHopTac", tbThongTinHopTac);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHinhThucHopTac"] = new SelectList(await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac"), "IdHinhThucHopTac", "HinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe"), "IdToChucHopTacQuocTe", "ToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTac = await ApiServices_.GetId<TbThongTinHopTac>("/api/htqt/ThongTinHoptac", id ?? 0);
            if (tbThongTinHopTac == null)
            {
                return NotFound();
            }
            ViewData["IdHinhThucHopTac"] = new SelectList(await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac"), "IdHinhThucHopTac", "HinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe"), "IdToChucHopTacQuocTe", "ToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
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
                    await ApiServices_.Update<TbThongTinHopTac>("/api/htqt/ThongTinHopTac", id, tbThongTinHopTac);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TbThongTinHopTacExists(tbThongTinHopTac.IdThongTinHopTac) == false)
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
            ViewData["IdHinhThucHopTac"] = new SelectList(await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac"), "IdHinhThucHopTac", "HinhThucHopTac", tbThongTinHopTac.IdHinhThucHopTac);
            ViewData["IdToChucHopTac"] = new SelectList(await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe"), "IdToChucHopTacQuocTe", "ToChucHopTacQuocTe", tbThongTinHopTac.IdToChucHopTac);
            return View(tbThongTinHopTac);
        }

        // GET: TbThongTinHopTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbThongTinHopTacs = await ApiServices_.GetAll<TbThongTinHopTac>("/api/htqt/ThongTinHopTac");
            var tbThongTinHopTac = tbThongTinHopTacs.FirstOrDefault(m => m.IdThongTinHopTac == id);
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
            await ApiServices_.Delete<TbThongTinHopTac>("/api/htqt/ThongTinHopTac", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbThongTinHopTacExists(int id)
        {
            var tbThongTinHopTacs = await ApiServices_.GetAll<TbThongTinHopTac>("/api/htqt/ThongTinHopTac");
            return tbThongTinHopTacs.Any(e => e.IdHinhThucHopTac == id);
        }
    }
}
