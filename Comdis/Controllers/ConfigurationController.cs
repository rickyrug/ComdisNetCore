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
    public class ConfigurationController : Controller
    {
        private readonly ComdisContext _context;

        public ConfigurationController(ComdisContext context)
        {
            _context = context;
        }

        // GET: Configuration
        public async Task<IActionResult> Index()
        {
            return View(await _context.Configuration.ToListAsync());
        }

        // GET: Configuration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configuration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // GET: Configuration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,code,value")] Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuration);
        }

        // GET: Configuration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configuration.FindAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }
            return View(configuration);
        }

        // POST: Configuration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,code,value")] Configuration configuration)
        {
            if (id != configuration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationExists(configuration.Id))
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
            return View(configuration);
        }

        // GET: Configuration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configuration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // POST: Configuration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuration = await _context.Configuration.FindAsync(id);
            _context.Configuration.Remove(configuration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigurationExists(int id)
        {
            return _context.Configuration.Any(e => e.Id == id);
        }
    }
}
