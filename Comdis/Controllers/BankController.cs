using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DataAccess.UnitOfWork;
using DataAccess.Models;
using Comdis.DataAccess.UnitOfWork;
using Comdis.Comdis.Controllers;

namespace Comdis.Controllers
{
    public class BankController : GenericController
    {
        public BankController(IUnitOfWork punitOfWork) : base(punitOfWork)
        {
        }

        // GET: Bank
        public IActionResult Index()
        {
            var banks = unitOfWork.Bank.Get();
            return View(banks);
        }

        // GET: Bank/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = unitOfWork.Bank.GetByID(id.Value);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Bank/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,AccountNumber,BranchCode")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                bank.GetType().GetProperty("Cretead").SetValue(bank, DateTime.Now);
                bank.GetType().GetProperty("CreatedBy").SetValue(bank, User.Identity.Name);
                bank.GetType().GetProperty("Updated").SetValue(bank, DateTime.Now);
                bank.GetType().GetProperty("UpdatedBy").SetValue(bank, User.Identity.Name);
                unitOfWork.Bank.Insert(bank);
                unitOfWork.Save();
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

            var bank = unitOfWork.Bank.GetByID(id.Value);
            if (bank == null)
            {
                return NotFound();
            }
            return View(bank);
        }

        // POST: Bank/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,AccountNumber,BranchCode")] Bank bank)
        {
            if (id != bank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Bank.Update(bank);
                    unitOfWork.Save();
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

            var bank = unitOfWork.Bank.GetByID(id.Value);   
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
            var bank = unitOfWork.Bank.GetByID(id);
            if (bank != null)
            {
                unitOfWork.Bank.Delete(bank);
                unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(int id)
        {
            return unitOfWork.Bank.GetByID(id) != null;    
        }
    }
}
