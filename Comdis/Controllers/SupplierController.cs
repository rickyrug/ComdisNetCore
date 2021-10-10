using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Comdis.Models;
using Comdis.Comdis.Models.VM;

namespace Comdis.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ComdisContext _context;

        public SupplierController(ComdisContext context)
        {
            _context = context;
        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            return View(await _context.Supplier
                               .Include(s => s.SuscribedBank)
                               .ToListAsync());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier.Include(s => s.SuscribedBank)
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Adress,Adress2,Phone1,Phone2,RFC,email,BankAccount,SuscribedBankId")] SupplierVM supplier)
        {
            if (ModelState.IsValid)
            {
                
                var var_suscribedBank = _context.Bank.Find(supplier.SuscribedBankId);

                _context.Add(new Supplier(){
                        Adress = supplier.Adress
                    ,   Adress2 = supplier.Adress2
                    ,   BankAccount = supplier.BankAccount
                    ,   CreatedBy   = ""
                    ,   Cretead     = DateTime.Now
                    ,   email       = supplier.email
                    ,   Name        = supplier.Name
                    ,   Phone1      = supplier.Phone1
                    ,   Phone2      = supplier.Phone2
                    ,   RFC         = supplier.RFC
                    ,   SuscribedBank   = var_suscribedBank
                    ,   Updated         = DateTime.Now
                    ,   UpdatedBy       = ""
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var var_supplier = await _context.Supplier.Include(s => s.SuscribedBank).FirstOrDefaultAsync(s => s.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Adress,Adress2,Phone1,Phone2,RFC,email,BankAccount,SuscribedBankId,CreatedBy,Cretead")] SupplierVM supplier)
        {
            var var_editSupplier = _context.Supplier.Find(supplier.Id);
            
            if (var_editSupplier == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var var_suscribedBank = _context.Bank.Find(supplier.SuscribedBankId);

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


                    _context.Update(var_editSupplier);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Supplier.Any(e => e.Id == id);
        }

        private void PopulateBankDropDown(object selectedItem = null)
        {
            var bankList = _context.Bank.AsNoTracking().ToList();
            ViewBag.bankList = new SelectList(bankList, "Id", "Name", selectedItem);

        }
    }
}
