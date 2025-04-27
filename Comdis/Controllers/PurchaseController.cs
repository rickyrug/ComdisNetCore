using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.UnitOfWork;
using DataAccess.Models;
using Comdis.Comdis.Controllers;
using Comdis.DataAccess.UnitOfWork;

namespace Comdis.Controllers
{
    public class PurchaseController : GenericController
    {
        public PurchaseController(IUnitOfWork punitOfWork) : base(punitOfWork)
        {
        }

        // GET: Purchase
        public IActionResult Index()
        {
            var list = this.unitOfWork.Purchase.Get();
              return View(list);
        }

        // GET: Purchase/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var purchase = this.unitOfWork.Purchase.GetByID(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchase/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RequestedDeliveryDate,DeliveryAdress,Comments,discount,discount2,discount3,tax")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.Purchase.Insert(purchase);
                this.unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(purchase);
        }

        // GET: Purchase/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = this.unitOfWork.Purchase.GetByID(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,RequestedDeliveryDate,DeliveryAdress,Comments,discount,discount2,discount3,tax")] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    this.unitOfWork.Purchase.Update(purchase);
                    this.unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.Id))
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
            return View(purchase);
        }

        // GET: Purchase/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var purchase = this.unitOfWork.Purchase.GetByID(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           
            var purchase = this.unitOfWork.Purchase.GetByID(id);
            if (purchase != null)
            {
                this.unitOfWork.Purchase.Delete(purchase);
            }
            
            this.unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
          return this.unitOfWork.Purchase.Exist(id);
        }
    }
}
