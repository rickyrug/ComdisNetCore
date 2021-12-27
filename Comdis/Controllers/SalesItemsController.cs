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
    public class SalesItemsController : Controller
    {
        private readonly ComdisContext _context;

        public SalesItemsController(ComdisContext context)
        {
            _context = context;
        }

        // GET: SalesItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesItems.ToListAsync());
        }

        // GET: SalesItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesItems = await _context.SalesItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesItems == null)
            {
                return NotFound();
            }

            return View(salesItems);
        }

        // GET: SalesItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Price,CreatedBy,Cretead,UpdatedBy,Updated")] SalesItems salesItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesItems);
        }

        // GET: SalesItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesItems = await _context.SalesItems.FindAsync(id);
            if (salesItems == null)
            {
                return NotFound();
            }
            return View(salesItems);
        }

        // POST: SalesItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Price,CreatedBy,Cretead,UpdatedBy,Updated")] SalesItems salesItems)
        {
            if (id != salesItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesItemsExists(salesItems.Id))
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
            return View(salesItems);
        }

        // GET: SalesItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesItems = await _context.SalesItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesItems == null)
            {
                return NotFound();
            }

            return View(salesItems);
        }

        // POST: SalesItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesItems = await _context.SalesItems.FindAsync(id);
            _context.SalesItems.Remove(salesItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesItemsExists(int id)
        {
            return _context.SalesItems.Any(e => e.Id == id);
        }
    }
}
