using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Models;
using DataAccess.UnitOfWork;
using DataAccess.Models;

namespace Comdis.Controllers
{
    public class UOMController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public UOMController(ComdisContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        // GET: UOM
        public IActionResult Index()
        {
            var items = this.unitOfWork.Uom.Get();
            return View(items);
        }

        // GET: UOM/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uOM = this.unitOfWork.Uom.GetByID(id);
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
        public IActionResult Create([Bind("Id,Name,Code")] UOM uOM)
        {
            if (ModelState.IsValid)
            {

                uOM.CreatedBy = "";
                uOM.UpdatedBy = "";
                uOM.Cretead = DateTime.Now;
                uOM.Updated = DateTime.Now;

                this.unitOfWork.Uom.Insert(uOM);
                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(uOM);
        }

        // GET: UOM/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uOM = this.unitOfWork.Uom.GetByID(id);
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
        public IActionResult Edit(int id, [Bind("Id,Name,Code")] UOM uOM)
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

                    
                    this.unitOfWork.Uom.Update(uOM);
                    this.unitOfWork.Save();
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uOM = this.unitOfWork.Uom.GetByID(id);
            if (uOM == null)
            {
                return NotFound();
            }

            return View(uOM);
        }

        // POST: UOM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var uOM = this.unitOfWork.Uom.GetByID(id);
            this.unitOfWork.Uom.Delete(uOM);
            this.unitOfWork.Save();
           
            return RedirectToAction(nameof(Index));
        }

        private bool UOMExists(int id)
        {
            return this.unitOfWork.Uom.Exist(id);
        }
    }
}
