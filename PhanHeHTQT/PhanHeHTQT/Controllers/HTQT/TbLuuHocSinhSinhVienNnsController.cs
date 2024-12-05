using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using PhanHeHTQT.API;
using PhanHeHTQT.Models;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Controllers.HTQT
{
    public class TbLuuHocSinhSinhVienNnsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbLuuHocSinhSinhVienNnsController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbLuuHocSinhSinhVienNn>> TbLuuHocSinhSinhVienNns(){
            List<TbLuuHocSinhSinhVienNn> tbLuuHocSinhSinhVienNns = await ApiServices_.GetAll<TbLuuHocSinhSinhVienNn>("/api/htqt/LuuHocSinhSinhVienNn");
            List<DmLoaiLuuHocSinh> dmLoaiLuuHocSinhs = await ApiServices_.GetAll<DmLoaiLuuHocSinh>("/api/dm/LoaiLuuHocSinh");
            List<DmNguonKinhPhiChoLuuHocSinh> dmNguonKinhPhiChoLuuHocSinhs = await ApiServices_.GetAll<DmNguonKinhPhiChoLuuHocSinh>("/api/dm/NguonKinhPhiChoLuuHocSinh");
            tbLuuHocSinhSinhVienNns.ForEach(item => { 
                item.IdLoaiLuuHocSinhNavigation = dmLoaiLuuHocSinhs.FirstOrDefault(x => x.IdLoaiLuuHocSinh == item.IdLoaiLuuHocSinh);
                item.IdNguonKinhPhiLuuHocSinhNavigation = dmNguonKinhPhiChoLuuHocSinhs.FirstOrDefault(x => x.IdNguonKinhPhiChoLuuHocSinh == item.IdNguonKinhPhiLuuHocSinh);
            });
            return tbLuuHocSinhSinhVienNns;
        }

        // GET: TbLuuHocSinhSinhVienNns
        public async Task<IActionResult> Index()
        {
            List<TbLuuHocSinhSinhVienNn> getall = await TbLuuHocSinhSinhVienNns();
            return View(getall);
        }

        // GET: TbLuuHocSinhSinhVienNns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLuuHocSinhSinhVienNns = await TbLuuHocSinhSinhVienNns();
            var tbLuuHocSinhSinhVienNn = tbLuuHocSinhSinhVienNns.FirstOrDefault(m => m.IdNguonKinhPhiLuuHocSinh == id);
            if (tbLuuHocSinhSinhVienNn == null)
            {
                return NotFound();
            }

            return View(tbLuuHocSinhSinhVienNn);
        }

        // GET: TbLuuHocSinhSinhVienNns/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdLoaiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmLoaiLuuHocSinh>("/api/dm/LoaiLuuHocSinh"), "IdLoaiLuuHocSinh", "IdLoaiLuuHocSinh");
            ViewData["IdNguonKinhPhiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoLuuHocSinh>("/api/dm/NguonKinhPhiChoLuuHocSinh"), "IdNguonKinhPhiChoLuuHocSinh", "IdNguonKinhPhiChoLuuHocSinh");
            return View();
        }

        // POST: TbLuuHocSinhSinhVienNns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLuuHocSinhSinhVienNn,IdNguoiHoc,IdNguonKinhPhiLuuHocSinh,IdLoaiLuuHocSinh")] TbLuuHocSinhSinhVienNn tbLuuHocSinhSinhVienNn)
        {
            if (await TbLuuHocSinhSinhVienNnExists(tbLuuHocSinhSinhVienNn.IdLuuHocSinhSinhVienNn)) ModelState.AddModelError("IdLuuHocSinhSinhVienNn", "Id này đã tồn tại!");
            if (ModelState.IsValid)
            {
                await ApiServices_.Create<TbLuuHocSinhSinhVienNn>("/api/htqt/LuuHocSinhSinhVienNn", tbLuuHocSinhSinhVienNn);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLoaiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmLoaiLuuHocSinh>("/api/dm/LoaiLuuHocSinh"), "IdLoaiLuuHocSinh", "IdLoaiLuuHocSinh", tbLuuHocSinhSinhVienNn.IdLoaiLuuHocSinh);
            ViewData["IdNguonKinhPhiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoLuuHocSinh>("/api/dm/NguonKinhPhiChoLuuHocSinh"), "IdNguonKinhPhiChoLuuHocSinh", "IdNguonKinhPhiChoLuuHocSinh", tbLuuHocSinhSinhVienNn.IdNguonKinhPhiLuuHocSinh);
            return View(tbLuuHocSinhSinhVienNn);
        }

        // GET: TbLuuHocSinhSinhVienNns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLuuHocSinhSinhVienNn = await ApiServices_.GetId<TbLuuHocSinhSinhVienNn>("/api/htqt/LuuHocSinhSinhVienNn", id ?? 0);
            if (tbLuuHocSinhSinhVienNn == null)
            {
                return NotFound();
            }
            ViewData["IdLoaiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmLoaiLuuHocSinh>("/api/dm/LoaiLuuHocSinh"), "IdLoaiLuuHocSinh", "IdLoaiLuuHocSinh", tbLuuHocSinhSinhVienNn.IdLoaiLuuHocSinh);
            ViewData["IdNguonKinhPhiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoLuuHocSinh>("/api/dm/NguonKinhPhiChoLuuHocSinh"), "IdNguonKinhPhiChoLuuHocSinh", "IdNguonKinhPhiChoLuuHocSinh", tbLuuHocSinhSinhVienNn.IdNguonKinhPhiLuuHocSinh);
            return View(tbLuuHocSinhSinhVienNn);
        }

        // POST: TbLuuHocSinhSinhVienNns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLuuHocSinhSinhVienNn,IdNguoiHoc,IdNguonKinhPhiLuuHocSinh,IdLoaiLuuHocSinh")] TbLuuHocSinhSinhVienNn tbLuuHocSinhSinhVienNn)
        {
            if (id != tbLuuHocSinhSinhVienNn.IdLuuHocSinhSinhVienNn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ApiServices_.Update<TbLuuHocSinhSinhVienNn>("/api/htqt/LuuHocSinhSinhVienNn", id, tbLuuHocSinhSinhVienNn);    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TbLuuHocSinhSinhVienNnExists(tbLuuHocSinhSinhVienNn.IdLuuHocSinhSinhVienNn) == false)
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
            ViewData["IdLoaiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmLoaiLuuHocSinh>("/api/dm/LoaiLuuHocSinh"), "IdLoaiLuuHocSinh", "IdLoaiLuuHocSinh", tbLuuHocSinhSinhVienNn.IdLoaiLuuHocSinh);
            ViewData["IdNguonKinhPhiLuuHocSinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoLuuHocSinh>("/api/dm/NguonKinhPhiChoLuuHocSinh"), "IdNguonKinhPhiChoLuuHocSinh", "IdNguonKinhPhiChoLuuHocSinh", tbLuuHocSinhSinhVienNn.IdNguonKinhPhiLuuHocSinh);
            return View(tbLuuHocSinhSinhVienNn);
        }

        // GET: TbLuuHocSinhSinhVienNns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLuuHocSinhSinhVienNns = await ApiServices_.GetAll<TbLuuHocSinhSinhVienNn>("/api/htqt/LuuHocSinhSinhVienNn");
            var tbLuuHocSinhSinhVienNn = tbLuuHocSinhSinhVienNns.FirstOrDefault(m => m.IdLuuHocSinhSinhVienNn == id);
            if (tbLuuHocSinhSinhVienNn == null)
            {
                return NotFound();
            }

            return View(tbLuuHocSinhSinhVienNn);
        }

        // POST: TbLuuHocSinhSinhVienNns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ApiServices_.Delete<TbLuuHocSinhSinhVienNn>("/api/htqt/LuuHocSinhSinhVienNn", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TbLuuHocSinhSinhVienNnExists(int id)
        {
            var tbLuuHocSinhSinhVienNns = await ApiServices_.GetAll<TbLuuHocSinhSinhVienNn>("/api/htqt/HoiThaoHoiNghi");
            return tbLuuHocSinhSinhVienNns.Any(e => e.IdLuuHocSinhSinhVienNn == id);
        }
    }
}
