using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Models;

namespace Comdis.Controllers
{
    public class UOMController : Controller
    {
        private readonly ComdisContext _context;

        public UOMController(ComdisContext context)
        {
            _context = context;
        }

        // GET: UOM
        public async Task<IActionResult> Index()
        {
            return View(await _context.UOM.ToListAsync());
        }

        // GET: UOM/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uOM = await _context.UOM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uOM == null)
            {
                return NotFound();
            }

            return View(uOM);
        }

        // GET: UOM/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UOM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code")] UOM uOM)
        {
            if (ModelState.IsValid)
            {

                uOM.CreatedBy = "";
                uOM.UpdatedBy = "";
                uOM.Cretead = DateTime.Now;
                uOM.Updated = DateTime.Now;

                _context.Add(uOM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uOM);
        }

        // GET: UOM/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uOM = await _context.UOM.FindAsync(id);
            if (uOM == null)
            {
                return NotFound();
            }
            return View(uOM);
        }

        // POST: UOM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code")] UOM uOM)
        {
            if (id != uOM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uOM.Updated = DateTime.Now;
                    uOM.UpdatedBy = "";

                    _context.Update(uOM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UOMExists(uOM.Id))
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
            return View(uOM);
        }

        // GET: UOM/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uOM = await _context.UOM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uOM == null)
            {
                return NotFound();
            }

            return View(uOM);
        }

        // POST: UOM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uOM = await _context.UOM.FindAsync(id);
            _context.UOM.Remove(uOM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UOMExists(int id)
        {
            return _context.UOM.Any(e => e.Id == id);
        }
    }
}
