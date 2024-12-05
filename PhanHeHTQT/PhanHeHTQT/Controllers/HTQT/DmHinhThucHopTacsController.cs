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

namespace C500Hemis.Controllers.HTQT
{
    public class DmHinhThucHopTacsController : Controller
    {
        private readonly ApiServices ApiServices_;

        public DmHinhThucHopTacsController(ApiServices services)
        {
            ApiServices_ = services;
        }

        private async Task<List<DmHinhThucHopTac>> DmHinhThucHopTacs()
        {
            List<DmHinhThucHopTac> dmHinhThucHopTacs = await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac");
            return dmHinhThucHopTacs;
        }

        // GET: DmHinhThucHopTacs
        public async Task<IActionResult> Index()
        {
            List<DmHinhThucHopTac> getall = await DmHinhThucHopTacs();
            return View(getall);
        }

        // GET: DmHinhThucHopTacs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dmHinhThucHopTacs = await DmHinhThucHopTacs();
            var dmHinhThucHopTac = dmHinhThucHopTacs.FirstOrDefault(m => m.IdHinhThucHopTac == id);
            if (dmHinhThucHopTac == null)
            {
                return NotFound();
            }

            return View(dmHinhThucHopTac);
        }

        // GET: DmHinhThucHopTacs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DmHinhThucHopTacs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHinhThucHopTac,HinhThucHopTac")] DmHinhThucHopTac dmHinhThucHopTac)
        {
            if (await DmHinhThucHopTacExists(dmHinhThucHopTac.IdHinhThucHopTac)) ModelState.AddModelError("IdHinhThucHopTac", "ID này đã tồn tại!");
            if (ModelState.IsValid)
            {
                await ApiServices_.Create<DmHinhThucHopTac>("/api/dm/HinhThucHopTac", dmHinhThucHopTac);
                return RedirectToAction(nameof(Index));
            }
            return View(dmHinhThucHopTac);
        }

        // GET: DmHinhThucHopTacs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmHinhThucHopTac = await ApiServices_.GetId<DmHinhThucHopTac>("/api/dm/HinhThucHopTac", id ?? 0);
            if (dmHinhThucHopTac == null)
            {
                return NotFound();
            }
            return View(dmHinhThucHopTac);
        }

        // POST: DmHinhThucHopTacs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHinhThucHopTac,HinhThucHopTac")] DmHinhThucHopTac dmHinhThucHopTac)
        {
            if (id != dmHinhThucHopTac.IdHinhThucHopTac)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ApiServices_.Update<DmHinhThucHopTac>("/api/dm/HinhThucHopTac", id, dmHinhThucHopTac);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await DmHinhThucHopTacExists(dmHinhThucHopTac.IdHinhThucHopTac))
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
            return View(dmHinhThucHopTac);
        }

        // GET: DmHinhThucHopTacs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dmHinhThucHopTacs = await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac");
            var dmHinhThucHopTac = dmHinhThucHopTacs.FirstOrDefault(m => m.IdHinhThucHopTac == id);
            if (dmHinhThucHopTac == null)
            {
                return NotFound();
            }

            return View(dmHinhThucHopTac);
        }

        // POST: DmHinhThucHopTacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await ApiServices_.Delete<DmHinhThucHopTac>("/api/dm/HinhThucHopTac", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DmHinhThucHopTacExists(int id)
        {
            var dmHinhThucHopTacs = await ApiServices_.GetAll<DmHinhThucHopTac>("/api/dm/HinhThucHopTac");
            return dmHinhThucHopTacs.Any(e => e.IdHinhThucHopTac == id);
        }
    }
}
