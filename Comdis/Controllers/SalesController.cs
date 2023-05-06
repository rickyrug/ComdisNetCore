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
using DataAccess.UnitOfWork;
using DataAccess.Models;

namespace Comdis.Controllers
{
    public class SalesController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public SalesController(ComdisContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        // GET: Sales
        public IActionResult  Index()
        {

            var items = this.unitOfWork.Sales.Get();

            return View(items);
        }

        // GET: Sales/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = this.unitOfWork.Sales.GetByID(id);
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
        public IActionResult Create([Bind("Id,SalesToPartyId,RequestedDeliveryDate,DeliveryAdress,Comments,discount,discount2,discount3,SalesToPartyId,productID,quantity,price,tax")] SalesVM sales)
        {
            decimal var_iva = 0;
            decimal var_desc1 = 0;
            decimal var_desc2 = 0;
            decimal var_desc3 = 0;


            if (ModelState.IsValid)
            {

                if (sales.tax)
                {
                    var taxvalue = this.unitOfWork.Configuration.Get(c => c.code == "iva",null).FirstOrDefault(); //_context.Configuration.Where(c => c.code == "iva").AsNoTracking().FirstOrDefault();
                    var_iva = CounterHelper.CalculatePercentage(taxvalue.value);
                }


                // convert discount to %

                var_desc1 = CounterHelper.CalculatePercentage(sales.discount.ToString());
                var_desc2 = CounterHelper.CalculatePercentage(sales.discount2.ToString());
                var_desc3 = CounterHelper.CalculatePercentage(sales.discount3.ToString());


                var var_customer = this.unitOfWork.Customer.GetByID(sales.SalesToPartyId); // _context.Customer.Find(sales.SalesToPartyId);

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

                this.unitOfWork.Sales.Insert(newSales);
                this.unitOfWork.Save();

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

            var sales =this.unitOfWork.Sales.GetByID(id);


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
        public IActionResult Edit(int id, [Bind("Id,SalesToPartyId,RequestedDeliveryDate,DeliveryAdress,Comments,discount,discount2,discount3,CreatedBy,Cretead,UpdatedBy,Updated,tax")] SalesVM sales)
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
                        var taxvalue = this.unitOfWork.Configuration.Get(c => c.code == "iva", null).FirstOrDefault();  //_context.Configuration.Where(c => c.code == "iva").AsNoTracking().FirstOrDefault();
                        var_iva = CounterHelper.CalculatePercentage(taxvalue.value);
                    }

                    // convert discount to %
                    var_desc1 = CounterHelper.CalculatePercentage(sales.discount.ToString());
                    var_desc2 = CounterHelper.CalculatePercentage(sales.discount2.ToString());
                    var_desc3 = CounterHelper.CalculatePercentage(sales.discount3.ToString());


                    var var_salesHeader = this.unitOfWork.Sales.GetByID(id);//_context.Sales.Where(s => s.Id == sales.Id).FirstOrDefault();

                   if(var_salesHeader != null)
                    {

                        var_salesHeader.discount = var_desc1;
                        var_salesHeader.discount2 = var_desc2;
                        var_salesHeader.discount3 = var_desc3;
                        var_salesHeader.Comments = sales.Comments;
                        var_salesHeader.RequestedDeliveryDate = sales.RequestedDeliveryDate;
                        var_salesHeader.SalesToParty = this.unitOfWork.Customer.GetByID(sales.SalesToPartyId);// _context.Customer.Find(sales.SalesToPartyId);
                        var_salesHeader.Updated = DateTime.Now;
                        var_salesHeader.UpdatedBy = "";
                        var_salesHeader.tax = var_iva;

                        this.unitOfWork.Sales.Update(var_salesHeader);
                        this.unitOfWork.Save();

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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = this.unitOfWork.Sales.GetByID(id);
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
                salesList = this.unitOfWork.Sales.Get(s => s.Id == filter.salesnumber, null).ToList();

                return View("Index", salesList);
            }

            if (filter.customerList != 0)
            {
                salesList = this.unitOfWork.Sales.Get(s => s.Id == filter.salesnumber, null).ToList();

                return View("Index", salesList);
            }


            salesList = this.unitOfWork.Sales.Get(s => s.Cretead >= filter.datefrom && s.Cretead <= filter.dateto.AddHours(23).AddMinutes(59).AddSeconds(59),null).ToList();

           


            return View("Index", salesList);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sales = this.unitOfWork.Sales.GetByID(id); //await _context.Sales.FindAsync(id);
            this.unitOfWork.Sales.Delete(sales);
            this.unitOfWork.Save();
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult PrintSalesOrder(int SOId)
        {
            return PartialView("_PrintSO");
        }

        private bool SalesExists(int id)
        {
            return this.unitOfWork.Sales.Exist(id);
        }

        private void PopulateCustomerDropDown(object selectedItem = null)
        {
            var customerList = this.unitOfWork.Customer.Get();
            ViewBag.customerList = new SelectList(customerList, "Id", "Name", selectedItem);

        }
    }
}
