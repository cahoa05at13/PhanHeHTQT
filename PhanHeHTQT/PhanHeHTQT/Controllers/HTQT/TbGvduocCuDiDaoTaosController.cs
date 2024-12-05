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
    public class TbGvduocCuDiDaoTaosController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbGvduocCuDiDaoTaosController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbGvduocCuDiDaoTao>> TbGvduocCuDiDaoTaos()
        {
            List<TbGvduocCuDiDaoTao> tbGvduocCuDiDaoTaos = await ApiServices_.GetAll<TbGvduocCuDiDaoTao>("/api/htqt/GVDuocCuDiDaoTao");
            List<TbCanBo> tbCanBos = await ApiServices_.GetAll<TbCanBo>("/api/cb/CanBo");
            List<DmHinhThucThamGiaGvduocCuDiDaoTao> dmHinhThucThamGiaGvduocCuDiDaoTaos = await ApiServices_.GetAll<DmHinhThucThamGiaGvduocCuDiDaoTao>("/api/dm/HinhThucThamGiaGvduocCuDiDaoTao");
            List<DmNguonKinhPhiCuaGvduocCuDiDaoTao> dmNguonKinhPhiCuaGvduocCuDiDaoTaos = await ApiServices_.GetAll<DmNguonKinhPhiCuaGvduocCuDiDaoTao>("/api/dm/NguonKinhPhiCuaGvduocCuDiDaoTao");
            List<DmQuocTich> dmQuocTiches = await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich");
            List<DmTinhTrangGiangVienDuocCuDiDaoTao> dmTinhTrangGiangVienDuocCuDiDaoTaos = await ApiServices_.GetAll<DmTinhTrangGiangVienDuocCuDiDaoTao>("/api/dm/TinhTrangGiangVienDuocCuDiDaoTao");
            tbGvduocCuDiDaoTaos.ForEach(item =>
            {
                item.IdCanBoNavigation = tbCanBos.FirstOrDefault(x => x.IdCanBo == item.IdCanBo);
                item.IdHinhThucThamGiaGvduocCuDiDaoTaoNavigation = dmHinhThucThamGiaGvduocCuDiDaoTaos.FirstOrDefault(x => x.IdHinhThucThamGiaGvduocCuDiDaoTao == item.IdHinhThucThamGiaGvduocCuDiDaoTao);
                item.IdNguonKinhPhiCuaGvNavigation = dmNguonKinhPhiCuaGvduocCuDiDaoTaos.FirstOrDefault(x => x.IdNguonKinhPhiCuaGvduocCuDiDaoTao == item.IdNguonKinhPhiCuaGv);
                item.IdQuocGiaDenNavigation = dmQuocTiches.FirstOrDefault(x => x.IdQuocTich == item.IdQuocGiaDen);
                item.IdTinhTrangGvduocCuDiDaoTaoNavigation = dmTinhTrangGiangVienDuocCuDiDaoTaos.FirstOrDefault(x => x.IdTinhTrangGiangVienDuocCuDiDaoTao == item.IdGvduocCuDiDaoTao);
            });
            return tbGvduocCuDiDaoTaos;
        }
        // GET: TbGvduocCuDiDaoTaos
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TbGvduocCuDiDaoTao> getall = await TbGvduocCuDiDaoTaos();
                return View(getall);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        // GET: TbGvduocCuDiDaoTaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbGvduocCuDiDaoTaos = await TbGvduocCuDiDaoTaos();
                var tbGvduocCuDiDaoTao = tbGvduocCuDiDaoTaos.FirstOrDefault(m => m.IdGvduocCuDiDaoTao == id);
                if (tbGvduocCuDiDaoTao == null)
                {
                    return NotFound();
                }

                return View(tbGvduocCuDiDaoTao);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
           
        }

        // GET: TbGvduocCuDiDaoTaos/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/CanBo"), "IdCanBo", "IdCanBo");
                ViewData["IdHinhThucThamGiaGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmHinhThucThamGiaGvduocCuDiDaoTao>("/api/dm/HinhThucThamGiaGvduocCuDiDaoTao"), "IdHinhThucThamGiaGvduocCuDiDaoTao", "IdHinhThucThamGiaGvduocCuDiDaoTao");
                ViewData["IdNguonKinhPhiCuaGv"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiCuaGvduocCuDiDaoTao>("/api/dm/NguonKinhPhiCuaGvduocCuDiDaoTao"), "IdNguonKinhPhiCuaGvduocCuDiDaoTao", "IdNguonKinhPhiCuaGvduocCuDiDaoTao");
                ViewData["IdQuocGiaDen"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich");
                ViewData["IdTinhTrangGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmTinhTrangGiangVienDuocCuDiDaoTao>("/api/dm/TinhTrangGiangVienDuocCuDiDaoTao"), "IdTinhTrangGiangVienDuocCuDiDaoTao", "IdTinhTrangGiangVienDuocCuDiDaoTao");
                return View();
            } catch (Exception ex) 
            { 
                return BadRequest(); 
            }
        }

        // POST: TbGvduocCuDiDaoTaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGvduocCuDiDaoTao,IdCanBo,IdHinhThucThamGiaGvduocCuDiDaoTao,IdQuocGiaDen,TenCoSoGiaoDucThamGiaOnn,ThoiGianBatDau,ThoiGianKetThuc,IdTinhTrangGvduocCuDiDaoTao,IdNguonKinhPhiCuaGv")] TbGvduocCuDiDaoTao tbGvduocCuDiDaoTao)
        {
            if (await TbGvduocCuDiDaoTaoExists(tbGvduocCuDiDaoTao.IdGvduocCuDiDaoTao)) ModelState.AddModelError("IdGvduocCuDiDaoTao", "ID này đã tồn tại!");
            if (ModelState.IsValid)
            {
                await ApiServices_.Create<TbGvduocCuDiDaoTao>("/api/htqt/GVDuocCuDiDaoTao", tbGvduocCuDiDaoTao);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/CanBo"), "IdCanBo", "IdCanBo", tbGvduocCuDiDaoTao.IdCanBo);
            ViewData["IdHinhThucThamGiaGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmHinhThucThamGiaGvduocCuDiDaoTao>("/api/dm/HinhThucThamGiaGvduocCuDiDaoTao"), "IdHinhThucThamGiaGvduocCuDiDaoTao", "IdHinhThucThamGiaGvduocCuDiDaoTao", tbGvduocCuDiDaoTao.IdHinhThucThamGiaGvduocCuDiDaoTao);
            ViewData["IdNguonKinhPhiCuaGv"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiCuaGvduocCuDiDaoTao>("/api/dm/NguonKinhPhiCuaGvduocCuDiDaoTao"), "IdNguonKinhPhiCuaGvduocCuDiDaoTao", "IdNguonKinhPhiCuaGvduocCuDiDaoTao", tbGvduocCuDiDaoTao.IdNguonKinhPhiCuaGv);
            ViewData["IdQuocGiaDen"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich", tbGvduocCuDiDaoTao.IdQuocGiaDen);
            ViewData["IdTinhTrangGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmTinhTrangGiangVienDuocCuDiDaoTao>("/api/dm/TinhTrangGiangVienDuocCuDiDaoTao"), "IdTinhTrangGiangVienDuocCuDiDaoTao", "IdTinhTrangGiangVienDuocCuDiDaoTao", tbGvduocCuDiDaoTao.IdTinhTrangGvduocCuDiDaoTao);
            return View(tbGvduocCuDiDaoTao);
        }

        // GET: TbGvduocCuDiDaoTaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbGvduocCuDiDaoTao = await ApiServices_.GetId<TbGvduocCuDiDaoTao>("/api/htqt/GvduocCuDiDaoTao", id ?? 0);
            if (tbGvduocCuDiDaoTao == null)
            {
                return NotFound();
            }
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/CanBo"), "IdCanBo", "IdCanBo", tbGvduocCuDiDaoTao.IdCanBo);
            ViewData["IdHinhThucThamGiaGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmHinhThucThamGiaGvduocCuDiDaoTao>("/api/dm/HinhThucThamGiaGvduocCuDiDaoTao"), "IdHinhThucThamGiaGvduocCuDiDaoTao", "IdHinhThucThamGiaGvduocCuDiDaoTao", tbGvduocCuDiDaoTao.IdHinhThucThamGiaGvduocCuDiDaoTao);
            ViewData["IdNguonKinhPhiCuaGv"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiCuaGvduocCuDiDaoTao>("/api/dm/NguonKinhPhiCuaGvduocCuDiDaoTao"), "IdNguonKinhPhiCuaGvduocCuDiDaoTao", "IdNguonKinhPhiCuaGvduocCuDiDaoTao", tbGvduocCuDiDaoTao.IdNguonKinhPhiCuaGv);
            ViewData["IdQuocGiaDen"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich", tbGvduocCuDiDaoTao.IdQuocGiaDen);
            ViewData["IdTinhTrangGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmTinhTrangGiangVienDuocCuDiDaoTao>("/api/dm/TinhTrangGiangVienDuocCuDiDaoTao"), "IdTinhTrangGiangVienDuocCuDiDaoTao", "IdTinhTrangGiangVienDuocCuDiDaoTao", tbGvduocCuDiDaoTao.IdTinhTrangGvduocCuDiDaoTao);
            return View(tbGvduocCuDiDaoTao);
        }

        // POST: TbGvduocCuDiDaoTaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGvduocCuDiDaoTao,IdCanBo,IdHinhThucThamGiaGvduocCuDiDaoTao,IdQuocGiaDen,TenCoSoGiaoDucThamGiaOnn,ThoiGianBatDau,ThoiGianKetThuc,IdTinhTrangGvduocCuDiDaoTao,IdNguonKinhPhiCuaGv")] TbGvduocCuDiDaoTao tbGvduocCuDiDaoTao)
        {
            if (id != tbGvduocCuDiDaoTao.IdGvduocCuDiDaoTao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ApiServices_.Update<TbGvduocCuDiDaoTao>("/api/htqt/GvduocCuDiDaoTao", id, tbGvduocCuDiDaoTao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ( await TbGvduocCuDiDaoTaoExists(tbGvduocCuDiDaoTao.IdGvduocCuDiDaoTao) == false)
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
            ViewData["IdCanBo"] = new SelectList(await ApiServices_.GetAll<TbCanBo>("/api/cb/CanBo"), "IdCanBo", "IdCanBo", tbGvduocCuDiDaoTao.IdCanBo);
            ViewData["IdHinhThucThamGiaGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmHinhThucThamGiaGvduocCuDiDaoTao>("/api/dm/HinhThucThamGiaGvduocCuDiDaoTao"), "IdHinhThucThamGiaGvduocCuDiDaoTao", "IdHinhThucThamGiaGvduocCuDiDaoTao", tbGvduocCuDiDaoTao.IdHinhThucThamGiaGvduocCuDiDaoTao);
            ViewData["IdNguonKinhPhiCuaGv"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiCuaGvduocCuDiDaoTao>("/api/dm/NguonKinhPhiCuaGvduocCuDiDaoTao"), "IdNguonKinhPhiCuaGvduocCuDiDaoTao", "IdNguonKinhPhiCuaGvduocCuDiDaoTao", tbGvduocCuDiDaoTao.IdNguonKinhPhiCuaGv);
            ViewData["IdQuocGiaDen"] = new SelectList(await ApiServices_.GetAll<DmQuocTich>("/api/dm/QuocTich"), "IdQuocTich", "IdQuocTich", tbGvduocCuDiDaoTao.IdQuocGiaDen);
            ViewData["IdTinhTrangGvduocCuDiDaoTao"] = new SelectList(await ApiServices_.GetAll<DmTinhTrangGiangVienDuocCuDiDaoTao>("/api/dm/TinhTrangGiangVienDuocCuDiDaoTao"), "IdTinhTrangGiangVienDuocCuDiDaoTao", "IdTinhTrangGiangVienDuocCuDiDaoTao", tbGvduocCuDiDaoTao.IdTinhTrangGvduocCuDiDaoTao);
            return View(tbGvduocCuDiDaoTao);
        }

        // GET: TbGvduocCuDiDaoTaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbGvduocCuDiDaoTaos = await ApiServices_.GetAll<TbGvduocCuDiDaoTao>("/api/htqt/GvduocCuDiDaoTao");
            var tbGvduocCuDiDaoTao = tbGvduocCuDiDaoTaos.FirstOrDefault(m => m.IdGvduocCuDiDaoTao == id);
            if (tbGvduocCuDiDaoTao == null)
            {
                return NotFound();
            }

            return View(tbGvduocCuDiDaoTao);
        }

        // POST: TbGvduocCuDiDaoTaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ApiServices_.Delete<TbGvduocCuDiDaoTao>("/api/htqt/GvduocCuDiDaoTao", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbGvduocCuDiDaoTaoExists(int id)
        {
            var tbGvduocCuDiDaoTaos = await ApiServices_.GetAll<TbGvduocCuDiDaoTao>("/api/htqt/GVDuocCuDiDaoTao");
            return tbGvduocCuDiDaoTaos.Any(e => e.IdGvduocCuDiDaoTao == id);
        }
    }
}

