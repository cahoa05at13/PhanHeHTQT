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
    public class TbDoanCongTacsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbDoanCongTacsController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbDoanCongTac>> TbDoanCongTacs()
        {
            List<TbDoanCongTac> tbDoanCongTacs = await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac");
            List<DmPhanLoaiDoanRaDoanVao> dmPhanLoaiDoanRaDoanVaos = await ApiServices_.GetAll<DmPhanLoaiDoanRaDoanVao>("/api/dm/PhanLoaiDoanRaDoanVao");
            List<DmQuocTich> dmQuocTiches = await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich");
            tbDoanCongTacs.ForEach(item =>
            {
                item.IdPhanLoaiDoanRaDoanVaoNavigation = dmPhanLoaiDoanRaDoanVaos.FirstOrDefault(x => x.IdPhanLoaiDoanRaDoanVao == item.IdPhanLoaiDoanRaDoanVao);
                item.IdQuocGiaDoanNavigation = dmQuocTiches.FirstOrDefault(x => x.IdQuocTich == item.IdQuocGiaDoan);
            });
            return tbDoanCongTacs;
        }

        // GET: TbDoanCongTacs
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TbDoanCongTac> getall = await TbDoanCongTacs();
                return View(getall);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbDoanCongTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbDoanCongTacs = await TbDoanCongTacs();
                var tbDoanCongTac = tbDoanCongTacs.FirstOrDefault(m => m.IdDoanCongTac == id);
                if (tbDoanCongTac == null)
                {
                    return NotFound();
                }

                return View(tbDoanCongTac);
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
                            
        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary> tạo danh sách 
        /// <returns></returns> 
        public async Task<IActionResult> Create()
        {
            try {
                ViewData["IdPhanLoaiDoanRaDoanVao"] = new SelectList(await ApiServices_.GetAll<DmPhanLoaiDoanRaDoanVao>("/api/dm/PhanLoaiDoanRaDoanVao"), "IdPhanLoaiDoanRaDoanVao", "IdPhanLoaiDoanRaDoanVao");
                ViewData["IdQuocGiaDoan"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich");
                return View();
            } 
            catch (Exception ex)
            { 
                return BadRequest(); 
            }            
        }

        // POST: TbDoanCongTacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDoanCongTac,MaDoanCongTac,IdPhanLoaiDoanRaDoanVao,TenDoanCongTac,SoQuyetDinh,NgayQuyetDinh,IdQuocGiaDoan,ThoiGianBatDau,ThoiGianketThuc,MucDichCongTac,KetQuaCongTac")] TbDoanCongTac tbDoanCongTac)
        {
            try
            {
                if (await TbDoanCongTacExists(tbDoanCongTac.IdDoanCongTac)) ModelState.AddModelError("IdDoanCongTac", "ID này đã tồn tại!");
                if (ModelState.IsValid)
                {
                    await ApiServices_.Create<TbDoanCongTac>("/api/htqt/DoanCongTac", tbDoanCongTac);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdPhanLoaiDoanRaDoanVao"] = new SelectList(await ApiServices_.GetAll<DmPhanLoaiDoanRaDoanVao>("/api/dm/PhanLoaiDoanRaDoanVao"), "IdPhanLoaiDoanRaDoanVao", "IdPhanLoaiDoanRaDoanVao", tbDoanCongTac.IdPhanLoaiDoanRaDoanVao);
                ViewData["IdQuocGiaDoan"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich", tbDoanCongTac.IdQuocGiaDoan);
                return View(tbDoanCongTac);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        // GET: TbDoanCongTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try {
                if (id == null)
                {
                    return NotFound();
                }

                var tbDoanCongTac = await ApiServices_.GetId<TbDoanCongTac>("/api/htqt/DoanCongTac", id ?? 0);
                if (tbDoanCongTac == null)
                {
                    return NotFound();
                }
                ViewData["IdPhanLoaiDoanRaDoanVao"] = new SelectList(await ApiServices_.GetAll<DmPhanLoaiDoanRaDoanVao>("/api/dm/PhanLoaiDoanRaDoanVao"), "IdPhanLoaiDoanRaDoanVao", "IdPhanLoaiDoanRaDoanVao", tbDoanCongTac.IdPhanLoaiDoanRaDoanVao);
                ViewData["IdQuocGiaDoan"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich", tbDoanCongTac.IdQuocGiaDoan);
                return View(tbDoanCongTac);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        // POST: TbDoanCongTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDoanCongTac,MaDoanCongTac,IdPhanLoaiDoanRaDoanVao,TenDoanCongTac,SoQuyetDinh,NgayQuyetDinh,IdQuocGiaDoan,ThoiGianBatDau,ThoiGianketThuc,MucDichCongTac,KetQuaCongTac")] TbDoanCongTac tbDoanCongTac)
        {
            try
            {
                if (id != tbDoanCongTac.IdDoanCongTac)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await ApiServices_.Update<TbDoanCongTac>("/api/htqt/DoanCongTac", id, tbDoanCongTac);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (await TbDoanCongTacExists(tbDoanCongTac.IdDoanCongTac) == false)
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
                ViewData["IdPhanLoaiDoanRaDoanVao"] = new SelectList(await ApiServices_.GetAll<DmPhanLoaiDoanRaDoanVao>("/api/dm/PhanLoaiDoanRaDoanVao"), "IdPhanLoaiDoanRaDoanVao", "IdPhanLoaiDoanRaDoanVao", tbDoanCongTac.IdPhanLoaiDoanRaDoanVao);
                ViewData["IdQuocGiaDoan"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich", tbDoanCongTac.IdQuocGiaDoan);
                return View(tbDoanCongTac);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbDoanCongTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var tbDoanCongTacs = await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac");
                var tbDoanCongTac = tbDoanCongTacs.FirstOrDefault(m => m.IdDoanCongTac == id);
                if (tbDoanCongTac == null)
                {
                    return NotFound();
                }

                return View(tbDoanCongTac);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        // POST: TbDoanCongTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await ApiServices_.Delete<TbDoanCongTac>("/api/htqt/DoanCongTac", id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private async Task<bool> TbDoanCongTacExists(int id)
        {
            var tbDoanCongTacs = await ApiServices_.GetAll<TbDoanCongTac>("/api/htqt/DoanCongTac");
            return tbDoanCongTacs.Any(e => e.IdDoanCongTac == id);
        }
    }
}
