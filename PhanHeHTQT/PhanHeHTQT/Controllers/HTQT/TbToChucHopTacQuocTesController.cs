using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanHeHTQT.API;
using PhanHeHTQT.Models;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbToChucHopTacQuocTesController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbToChucHopTacQuocTesController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbToChucHopTacQuocTe>> TbToChucHopTacQuocTes()
        {
            List<TbToChucHopTacQuocTe> tbToChucHopTacQuocTes = await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe");
            List<DmQuocTich> dmQuocTiches = await ApiServices_.GetAll<DmQuocTich>("/api/htqt/QuocTich");
            List<DmHinhThucHopTac> dmHinhThucHopTacs = await ApiServices_.GetAll<DmHinhThucHopTac>("/api/htqt/HinhThucHopTac");
            tbToChucHopTacQuocTes.ForEach(item =>
            {
                item.IdQuocGiaNavigation = dmQuocTiches.FirstOrDefault(x => x.IdQuocTich == item.IdQuocGia);
            });
            return tbToChucHopTacQuocTes;
        }

        // GET: TbToChucHopTacQuocTes
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TbToChucHopTacQuocTe> getall = await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe");
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
            var tbToChucHopTacQuocTes = await TbToChucHopTacQuocTes();
            var tbToChucHopTacQuocTe = tbToChucHopTacQuocTes.FirstOrDefault(m => m.IdToChucHopTacQuocTe == id);
            if (tbToChucHopTacQuocTe == null)
            {
                return NotFound();
            }

            return View(tbToChucHopTacQuocTe);
        }

        // GET: TbToChucHopTacQuocTes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich");
            ViewData["IdHinhThucHopTac"] = new SelectList(await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac"), "IdHinhThucHopTac", "TenHinhThuc"); // Adjust the fields as necessary



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
                await ApiServices_.Create<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacDoanhNghiep", tbToChucHopTacQuocTe);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich");
            ViewData["IdHinhThucHopTac"] = new SelectList(await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac"), "IdHinhThucHopTac", "TenHinhThuc");
            return View(tbToChucHopTacQuocTe);
        }

        // GET: TbToChucHopTacQuocTes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbToChucHopTacQuocTe = await ApiServices_.GetId<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe", id ?? 0);
            if (tbToChucHopTacQuocTe == null)
            {
                return NotFound();
            }
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich");
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
                    await ApiServices_.Update<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe", id, tbToChucHopTacQuocTe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TbToChucHopTacQuocTeExists(tbToChucHopTacQuocTe.IdToChucHopTacQuocTe) == false)
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
            ViewData["IdQuocGia"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich");
            return View(tbToChucHopTacQuocTe);
        }

        // GET: TbToChucHopTacQuocTes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tbToChucHopTacQuocTes = await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe");
            var tbToChucHopTacQuocTe = tbToChucHopTacQuocTes.FirstOrDefault(m => m.IdToChucHopTacQuocTe == id);
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
            await ApiServices_.Delete<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbToChucHopTacQuocTeExists(int id)
        {
            var tbToChucHopTacQuocTes = await ApiServices_.GetAll<TbToChucHopTacQuocTe>("/api/htqt/ToChucHopTacQuocTe");
            return tbToChucHopTacQuocTes.Any(e => e.IdToChucHopTacQuocTe == id);
        }
    }
}
