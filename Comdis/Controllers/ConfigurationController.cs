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
using Comdis.DataAccess.UnitOfWork;

namespace Comdis.Controllers
{
    public class ConfigurationController : Controller
    {


        private readonly IUnitOfWork unitOfWork;

        public ConfigurationController(IUnitOfWork punitOfWork)
        {
            this.unitOfWork = punitOfWork;
        }


        // GET: Configuration
        public IActionResult Index()
        {
            var items = this.unitOfWork.Configuration.Get();
            return View(items);
        }

        // GET: Configuration/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = this.unitOfWork.Configuration.GetByID(id);
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
        public IActionResult Create([Bind("Id,code,value")] Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.Configuration.Insert(configuration);
                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(configuration);
        }

        // GET: Configuration/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = this.unitOfWork.Configuration.GetByID(id);
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
        public  IActionResult Edit(int id, [Bind("Id,code,value")] Configuration configuration)
        {
            if (id != configuration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.unitOfWork.Configuration.Update(configuration);
                    this.unitOfWork.Save();
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = this.unitOfWork.Configuration.GetByID(id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // POST: Configuration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var configuration = this.unitOfWork.Configuration.GetByID(id);
            this.unitOfWork.Configuration.Delete(configuration);
            this.unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigurationExists(int id)
        {
            return this.unitOfWork.Configuration.Exist(id);
        }
    }
}
