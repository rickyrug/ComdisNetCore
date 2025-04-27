using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Comdis.Models.VM;
using DataAccess.Models;
using Comdis.Comdis.Controllers;
using Comdis.DataAccess.UnitOfWork;

namespace Comdis.Controllers
{
    public class SupplierController : GenericController
    {
        public SupplierController(IUnitOfWork punitOfWork) : base(punitOfWork)
        {
        }

        // GET: Supplier
        public IActionResult Index()
        {
            var items = this.unitOfWork.Supplier.Get();
            return View(items);
        }

        // GET: Supplier/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = this.unitOfWork.Supplier.GetByID(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            this.PopulateBankDropDown();
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Adress,Adress2,Phone1,Phone2,RFC,email,BankAccount,SuscribedBankId")] SupplierVM supplier)
        {
            if (ModelState.IsValid)
            {
                
                var var_suscribedBank = this.unitOfWork.Bank.GetByID(supplier.SuscribedBankId);

                this.unitOfWork.Supplier.Insert(
                     new Supplier()
                     {
                         Adress = supplier.Adress
                    ,
                         Adress2 = supplier.Adress2
                    ,
                         BankAccount = supplier.BankAccount
                    ,
                         CreatedBy = ""
                    ,
                         Cretead = DateTime.Now
                    ,
                         email = supplier.email
                    ,
                         Name = supplier.Name
                    ,
                         Phone1 = supplier.Phone1
                    ,
                         Phone2 = supplier.Phone2
                    ,
                         RFC = supplier.RFC
                    ,
                         SuscribedBank = var_suscribedBank
                    ,
                         Updated = DateTime.Now
                    ,
                         UpdatedBy = ""
                     }
                    );


                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var var_supplier = this.unitOfWork.Supplier.GetByID(id);
            if (var_supplier == null)
            {
                return NotFound();
            }

            this.PopulateBankDropDown(var_supplier.SuscribedBank.Id);

            return View(new SupplierVM(){
                    Adress  = var_supplier.Adress
                ,   Adress2 = var_supplier.Adress2
                ,   BankAccount = var_supplier.BankAccount
                ,   CreatedBy   = var_supplier.CreatedBy
                ,   Cretead     = var_supplier.Cretead
                ,   email       = var_supplier.email
                ,   Id          = var_supplier.Id
                ,   Name        = var_supplier.Name
                ,   Phone1      = var_supplier.Phone1
                ,   Phone2      = var_supplier.Phone2
                ,   RFC         = var_supplier.RFC
                ,   SuscribedBankId = var_supplier.SuscribedBank.Id
            
            });
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Adress,Adress2,Phone1,Phone2,RFC,email,BankAccount,SuscribedBankId,CreatedBy,Cretead")] SupplierVM supplier)
        {
            var var_editSupplier = unitOfWork.Supplier.GetByID(id);
            
            if (var_editSupplier == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var var_suscribedBank = var_editSupplier.SuscribedBank;

                        var_editSupplier.Adress  = supplier.Adress;
                        var_editSupplier.Adress2 = supplier.Adress2;
                        var_editSupplier.BankAccount = supplier.BankAccount;
                        var_editSupplier.CreatedBy   = supplier.CreatedBy;
                        var_editSupplier.Cretead     = supplier.Cretead;
                        var_editSupplier.email       = supplier.email;
                        var_editSupplier.Name        = supplier.Name;
                        var_editSupplier.Phone1      = supplier.Phone1;
                        var_editSupplier.Phone2      = supplier.Phone2;
                        var_editSupplier.RFC         = supplier.RFC;
                        var_editSupplier.SuscribedBank  = var_suscribedBank;
                        var_editSupplier.Updated        = DateTime.Now;
                        var_editSupplier.UpdatedBy      = "";


                    this.unitOfWork.Supplier.Update(var_editSupplier);
                    this.unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = this.unitOfWork.Supplier.GetByID(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var supplier = this.unitOfWork.Supplier.GetByID(id);
            this.unitOfWork.Supplier.Delete(supplier);
            this.unitOfWork.Save();
         
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return this.unitOfWork.Supplier.Exist(id);
        }

        private void PopulateBankDropDown(object selectedItem = null)
        {
            var bankList = this.unitOfWork.Bank.Get();
            ViewBag.bankList = new SelectList(bankList, "Id", "Name", selectedItem);

        }
    }
}
