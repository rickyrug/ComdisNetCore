using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Models;
using Comdis.Models.VM;
using Comdis.Helpers;
using System.Collections.Generic;

namespace Comdis.Controllers
{
    public class SalesController : Controller
    {
        private readonly ComdisContext _context;

        public SalesController(ComdisContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult>  Index()
        {

           

            return View(await _context.Sales.
                                        Include(sales => sales.SalesToParty)
                                        .OrderByDescending(x => x.Id)
                                        .AsNoTracking()
                                        .ToListAsync()
                                );
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            SalesVM newsales = new SalesVM();
            newsales.RequestedDeliveryDate = DateTime.Now;
            newsales.discount = 0;
            newsales.discount2 = 0;
            newsales.discount3 = 0;
            return View(newsales);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SalesToPartyId,RequestedDeliveryDate,DeliveryAdress,Comments,discount,discount2,discount3,SalesToPartyId,productID,quantity,price,tax")] SalesVM sales)
        {
            decimal var_iva = 0;
            decimal var_desc1 = 0;
            decimal var_desc2 = 0;
            decimal var_desc3 = 0;


            if (ModelState.IsValid)
            {

                if (sales.tax)
                {
                    var taxvalue = _context.Configuration.Where(c => c.code == "iva").AsNoTracking().FirstOrDefault();
                    var_iva = CounterHelper.CalculatePercentage(taxvalue.value);
                }


                // convert discount to %

                var_desc1 = CounterHelper.CalculatePercentage(sales.discount.ToString());
                var_desc2 = CounterHelper.CalculatePercentage(sales.discount2.ToString());
                var_desc3 = CounterHelper.CalculatePercentage(sales.discount3.ToString());


                var var_customer = _context.Customer.Find(sales.SalesToPartyId);

                Sales newSales = new Sales() {
                    Comments = sales.Comments
                    , CreatedBy = ""
                    , Cretead = DateTime.Now
                    , DeliveryAdress = sales.DeliveryAdress
                    , discount = var_desc1
                    , discount2 = var_desc2
                    , discount3 = var_desc3
                    , RequestedDeliveryDate = sales.RequestedDeliveryDate
                    , SalesToParty = var_customer
                    , Updated = DateTime.Now
                    , UpdatedBy = ""
                    , tax = var_iva
                };


                _context.Add(newSales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateCustomerDropDown(sales.SalesToPartyId);

            return View(sales);
        }

        // GET: Sales/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales =_context.Sales.
                                        Include(sales => sales.SalesToParty).
                                        AsNoTracking().
                                        FirstOrDefault(header => header.Id == id);


            if (sales == null)
            {
                return NotFound();
            }

            if(sales.SalesToParty == null)
            {
                PopulateCustomerDropDown();

            }
            else
            {
                PopulateCustomerDropDown(sales.SalesToParty.Id);
            }

            SalesVM salesHeader = new SalesVM() {
                     Comments = sales.Comments
                    ,DeliveryAdress = sales.DeliveryAdress
                    ,discount               = sales.discount * 100
                    ,discount2              = sales.discount2 * 100
                    ,discount3              = sales.discount3 * 100
                    ,Id                            = sales.Id
                    ,RequestedDeliveryDate  = sales.RequestedDeliveryDate
                    ,SalesToPartyId     = sales.SalesToParty.Id
                    ,tax                            = sales.tax != 0 ? true : false
            };

         
            
            return View(salesHeader);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SalesToPartyId,RequestedDeliveryDate,DeliveryAdress,Comments,discount,discount2,discount3,CreatedBy,Cretead,UpdatedBy,Updated,tax")] SalesVM sales)
        {
            if (id != sales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    decimal var_iva = 0;
                    decimal var_desc1 = 0;
                    decimal var_desc2 = 0;
                    decimal var_desc3 = 0;

                    if (sales.tax)
                    {
                        var taxvalue = _context.Configuration.Where(c => c.code == "iva").AsNoTracking().FirstOrDefault();
                        var_iva = CounterHelper.CalculatePercentage(taxvalue.value);
                    }

                    // convert discount to %
                    var_desc1 = CounterHelper.CalculatePercentage(sales.discount.ToString());
                    var_desc2 = CounterHelper.CalculatePercentage(sales.discount2.ToString());
                    var_desc3 = CounterHelper.CalculatePercentage(sales.discount3.ToString());
 

                    var var_salesHeader = _context.Sales.Where(s => s.Id == sales.Id).FirstOrDefault();

                   if(var_salesHeader != null)
                    {

                        var_salesHeader.discount = var_desc1;
                        var_salesHeader.discount2 = var_desc2;
                        var_salesHeader.discount3 = var_desc3;
                        var_salesHeader.Comments = sales.Comments;
                        var_salesHeader.RequestedDeliveryDate = sales.RequestedDeliveryDate;
                        var_salesHeader.SalesToParty = _context.Customer.Find(sales.SalesToPartyId);
                        var_salesHeader.Updated = DateTime.Now;
                        var_salesHeader.UpdatedBy = "";
                        var_salesHeader.tax = var_iva;


                        _context.Update(var_salesHeader);
                        await _context.SaveChangesAsync();

                    }

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.Id))
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
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        [HttpPost]
        public IActionResult SearchFilters([Bind("datefrom,dateto,salesnumber,customerList")]SalesFilterVM filter)
        {
            List<Sales> salesList = new List<Sales>();
            if (filter.salesnumber != 0)
            {
                salesList =    _context.Sales
                               .Include(sales => sales.SalesToParty)
                               .Where(s => s.Id == filter.salesnumber)
                               .AsNoTracking()
                               .ToList();
                return View("Index", salesList);
            }

            if (filter.customerList != 0)
            {
                salesList = _context.Sales
                             .Include(sales => sales.SalesToParty)
                             .Where(s => s.SalesToParty.Id == filter.customerList)
                             .AsNoTracking()
                             .ToList();
                return View("Index", salesList);
            }


            salesList = _context.Sales
                                .Include(sales => sales.SalesToParty)
                                .Where(s => s.Cretead >= filter.datefrom && s.Cretead <= filter.dateto.AddHours(23).AddMinutes(59).AddSeconds(59))
                                .AsNoTracking()
                                .ToList();

           


            return View("Index", salesList);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }

        private void PopulateCustomerDropDown(object selectedItem = null)
        {
            var customerList = _context.Customer.AsNoTracking().ToList();
            ViewBag.customerList = new SelectList(customerList, "Id", "Name", selectedItem);

        }
    }
}
