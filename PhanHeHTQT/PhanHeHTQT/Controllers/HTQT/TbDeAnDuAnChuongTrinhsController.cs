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
    public class TbDeAnDuAnChuongTrinhsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public TbDeAnDuAnChuongTrinhsController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<TbDeAnDuAnChuongTrinh>> TbDeAnDuAnChuongTrinhs()
        {
            List<TbDeAnDuAnChuongTrinh> tbDeAnDuAnChuongTrinhs = await ApiServices_.GetAll<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh");
            List<DmNguonKinhPhiChoDeAn> dmNguonKinhPhiChoDeAns = await ApiServices_.GetAll<DmNguonKinhPhiChoDeAn>("/api/dm/NguonKinhPhiChoDeAn");
            tbDeAnDuAnChuongTrinhs.ForEach(item =>
            {
                item.IdNguonKinhPhiDeAnDuAnChuongTrinhNavigation = dmNguonKinhPhiChoDeAns.FirstOrDefault(x => x.IdNguonKinhPhiChoDeAn == item.IdNguonKinhPhiDeAnDuAnChuongTrinh);
            });
            return tbDeAnDuAnChuongTrinhs;
        }
            // GET: TbDeAnDuAnChuongTrinhs
            public async Task<IActionResult> Index()
        {
            try
            {
                List<TbDeAnDuAnChuongTrinh> getall = await ApiServices_.GetAll<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh");
                // Lấy data từ các table khác có liên quan (khóa ngoài) để hiển thị trên Index
                return View(getall);

                // Bắt lỗi các trường hợp ngoại lệ
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbDeAnDuAnChuongTrinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                // Tìm các dữ liệu theo Id tương ứng đã truyền vào view Details
                var TbDeAnDuAnChuongTrinhs = await ApiServices_.GetAll<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh");
                var TbDeAnDuAnChuongTrinh = TbDeAnDuAnChuongTrinhs.FirstOrDefault(m => m.IdDeAnDuAnChuongTrinh == id);
                // Nếu không tìm thấy Id tương ứng, chương trình sẽ báo lỗi NotFound
                if (TbDeAnDuAnChuongTrinh == null)
                {
                    return NotFound();
                }
                // Nếu đã tìm thấy Id tương ứng, chương trình sẽ dẫn đến view Details
                // Hiển thị thông thi chi tiết CTĐT thành công
                return View(TbDeAnDuAnChuongTrinh);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: TbDeAnDuAnChuongTrinhs/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["IdNguonKinhPhiDeAnDuAnChuongTrinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoDeAn>("/api/dm/NguonKinhPhiChoDeAn"), "IdNguonKinhPhiChoDeAn", "IdNguonKinhPhiChoDeAn");
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: TbDeAnDuAnChuongTrinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDeAnDuAnChuongTrinh,MaDeAnDuAnChuongTrinh,TenDeAnDuAnChuongTrinh,NoiDungTomTat,MucTieu,ThoiGianHopTacTu,ThoiGianHopTacDen,TongKinhPhi,IdNguonKinhPhiDeAnDuAnChuongTrinh")] TbDeAnDuAnChuongTrinh tbDeAnDuAnChuongTrinh)
        {
            try
            {
                if (await TbDeAnDuAnChuongTrinhExists(tbDeAnDuAnChuongTrinh.IdDeAnDuAnChuongTrinh)) ModelState.AddModelError("IdDeAnDuAnChuongTrinh", "ID này đã tồn tại!");
                if (ModelState.IsValid)
                {
                    await ApiServices_.Create<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh", tbDeAnDuAnChuongTrinh);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["IdNguonKinhPhiDeAnDuAnChuongTrinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoDeAn>("/api/htqt/DeAnDuAnChuongTrinh"), "IdNguonKinhPhiChoDeAn", "IdNguonKinhPhiChoDeAn", tbDeAnDuAnChuongTrinh.IdNguonKinhPhiDeAnDuAnChuongTrinh);
                return View(tbDeAnDuAnChuongTrinh);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET: TbDeAnDuAnChuongTrinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDeAnDuAnChuongTrinh = await ApiServices_.GetId<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh", id ?? 0);
            if (tbDeAnDuAnChuongTrinh == null)
            {
                return NotFound();
            }
            ViewData["IdNguonKinhPhiDeAnDuAnChuongTrinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoDeAn>("/api/htqt/DeAnDuAnChuongTrinh"), "IdNguonKinhPhiChoDeAn", "IdNguonKinhPhiChoDeAn", tbDeAnDuAnChuongTrinh.IdNguonKinhPhiDeAnDuAnChuongTrinh);
            return View(tbDeAnDuAnChuongTrinh);
        }

        // POST: TbDeAnDuAnChuongTrinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDeAnDuAnChuongTrinh,MaDeAnDuAnChuongTrinh,TenDeAnDuAnChuongTrinh,NoiDungTomTat,MucTieu,ThoiGianHopTacTu,ThoiGianHopTacDen,TongKinhPhi,IdNguonKinhPhiDeAnDuAnChuongTrinh")] TbDeAnDuAnChuongTrinh tbDeAnDuAnChuongTrinh)
        {
            try
            {
                if (id != tbDeAnDuAnChuongTrinh.IdDeAnDuAnChuongTrinh)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await ApiServices_.Update<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh", id, tbDeAnDuAnChuongTrinh);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (await TbDeAnDuAnChuongTrinhExists(tbDeAnDuAnChuongTrinh.IdDeAnDuAnChuongTrinh) == false)
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
                ViewData["IdNguonKinhPhiDeAnDuAnChuongTrinh"] = new SelectList(await ApiServices_.GetAll<DmNguonKinhPhiChoDeAn>("/api/htqt/DeAnDuAnChuongTrinh"), "IdNguonKinhPhiChoDeAn", "IdNguonKinhPhiChoDeAn", tbDeAnDuAnChuongTrinh.IdNguonKinhPhiDeAnDuAnChuongTrinh);
                return View(tbDeAnDuAnChuongTrinh);
            } 
            catch (Exception ex){
                return BadRequest();
                }
            }

        // GET: TbDeAnDuAnChuongTrinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbDeAnDuAnChuongTrinhs = await ApiServices_.GetAll<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh");
                var tbDeAnDuAnChuongTrinh = tbDeAnDuAnChuongTrinhs.FirstOrDefault(m => m.IdDeAnDuAnChuongTrinh == id);
                if (tbDeAnDuAnChuongTrinh == null)
                {
                    return NotFound();
                }

                return View(tbDeAnDuAnChuongTrinh);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST: TbDeAnDuAnChuongTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await ApiServices_.Delete<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh", id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        private async Task<bool> TbDeAnDuAnChuongTrinhExists(int id)
        {
            var tbDeAnDuAnChuongTrinh = await ApiServices_.GetAll<TbDeAnDuAnChuongTrinh>("/api/htqt/DeAnDuAnChuongTrinh");
            return tbDeAnDuAnChuongTrinh.Any(e => e.IdDeAnDuAnChuongTrinh == id);
        }
    }
}
