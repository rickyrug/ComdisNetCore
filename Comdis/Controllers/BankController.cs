using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using DataAccess.UnitOfWork;
using DataAccess.Models;

namespace Comdis.Controllers
{
    public class BankController : Controller
    {

        private readonly UnitOfWork unitOfwork;

      

        public BankController(ComdisContext context)
        {
            this.unitOfwork = new UnitOfWork(context);
            
        }

        // GET: Bank
        public  ActionResult Index()
        {
           var items =  this.unitOfwork.Bank.Get();
            return View(items);
        }

        // GET: Bank/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = this.unitOfwork.Bank.GetByID(id);

           
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Bank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bank/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                bank.Cretead = DateTime.Now;
                bank.CreatedBy = "";
                bank.Updated = DateTime.Now;
                bank.UpdatedBy = "";


                this.unitOfwork.Bank.Insert(bank);
                this.unitOfwork.Save();
               
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }

        // GET: Bank/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = this.unitOfwork.Bank.GetByID(id);
            if (bank == null)
            {
                return NotFound();
            }
            return View(bank);
        }

        // POST: Bank/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Cretead,CreatedBy")] Bank bank)
        {
            if (id != bank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    bank.Updated = DateTime.Now;
                    bank.UpdatedBy = "";

                    this.unitOfwork.Bank.Update(bank);
                    this.unitOfwork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.Id))
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
            return View(bank);
        }

        // GET: Bank/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = this.unitOfwork.Bank.GetByID(id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bank = unitOfwork.Bank.GetByID(id);
            unitOfwork.Bank.Delete(bank);
            unitOfwork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(int id)
        {
            return this.unitOfwork.Bank.Exist(id);
        }
    }
}
