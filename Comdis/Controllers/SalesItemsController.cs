using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Models;
using Comdis.Models.VM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Comdis.Helpers;

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
        public IActionResult Index(int id)
        {

            ViewBag.So = id.ToString();

            return View();
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
        public JsonResult Create([Bind("Quantity,Price,SOId,ProductId")] SalesItemVM salesItems)
        {
            
            try
            {
                


                if (ModelState.IsValid)
                {
                   // var var_so = _context.Sales.AsNoTracking().Where(so => so.Id == ).FirstOrDefault();
                   // var var_product = _context.Product.AsNoTracking().Where(pro => pro.Id == salesItems.ProductId).FirstOrDefault();
                    SalesItems newItem = new SalesItems();
                    newItem.SalesHeader = _context.Sales.Find(salesItems.SOId);
                    newItem.Product = _context.Product.Find(salesItems.ProductId);


                    newItem.Price = salesItems.Price;
                    newItem.Quantity = salesItems.Quantity;
                    


                    _context.Add(newItem);
                    _context.SaveChanges();

                    salesItems.uid = newItem.Id;
                    salesItems.ProductName = newItem.Product.Name;

                    return Json(new MessageVM<SalesItemVM>() { hasError = false, Message = salesItems });
                }


                var validation = ModelState.ToList();
                List<FormField> errorsInForm = new List<FormField>();
                errorsInForm = FormValidationHelper.processErrorsInForm(validation);
                return Json(new MessageVM<List<FormField>>() { hasError = true, Message = errorsInForm });

               

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception("Error When Creating Item" + ex.InnerException);
                }
                else
                {
                    throw new Exception("Error When Creating Item" + ex.Message);
                }  
                
            }
            
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


        public IActionResult GetCreateItemView(int pSOI)
        {
            SalesItemVM soi = new SalesItemVM();

            soi.SOId = pSOI;

            return PartialView("_CreateSOI", soi);
        }

        

        [HttpPost]
        public JsonResult GetSalesOrderItem(int id)
        {
            SalesVM SOI = new SalesVM();

            try
            {
                var SO = _context.Sales
                                .Include(soi => soi.SalesItems)
                                .ThenInclude(soi => soi.Product)
                                .Include(cu => cu.SalesToParty)
                                .AsNoTracking()
                                .Where(sa => sa.Id == id).FirstOrDefault();

                if (SO != null)
                {
                    SOI.Comments = SO.Comments;
                    SOI.DeliveryAdress = SO.DeliveryAdress;
                    SOI.discount = SO.discount;
                    SOI.discount2 = SO.discount2;
                    SOI.discount3 = SO.discount3;
                    SOI.Id = SO.Id;
                    SOI.RequestedDeliveryDate = SO.RequestedDeliveryDate;
                    SOI.SalesToPartyId = SO.SalesToParty.Id;
                    SOI.CustomerName = SO.SalesToParty.Name;
                    SOI.CreatedBy = SO.CreatedBy;
                    SOI.Cretead = SO.Cretead;
                    SOI.Updated = SO.Updated;
                    SOI.UpdatedBy = SO.UpdatedBy;


                    if (SO.SalesItems != null)
                    {
                        SOI.salesItem = new List<SalesItemVM>();

                        foreach (var item in SO.SalesItems)
                        {

                            SOI.salesItem.Add(new SalesItemVM
                            {
                                Price = item.Price,
                                ProductId = item.Product.Id,
                                ProductName = item.Product.Name,
                                Quantity = item.Quantity,
                                Cretead = item.Cretead,
                                CreatedBy = item.CreatedBy,
                                Updated = item.Updated,
                                UpdatedBy = item.UpdatedBy,
                                uid = item.Id
                            });
                        }
                    }
                }
                return Json(SOI);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public JsonResult deleteItem(int pId)
        {
            throw new Exception("Not implemented");
        }

        private bool SalesItemsExists(int id)
        {
            return _context.SalesItems.Any(e => e.Id == id);
        }
    }
}
