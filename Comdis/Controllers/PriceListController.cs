using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using DataAccess.UnitOfWork;
using Comdis.Models.VM;
using DataAccess.Models;


namespace Comdis.Controllers
{
    public class PriceListController : Controller
    {

        private readonly UnitOfWork unitOfwork;

      

        public PriceListController(ComdisContext context)
        {
            this.unitOfwork = new UnitOfWork(context);
            
        }

        // GET: PriceList
        public  ActionResult Index()
        {
           var items =  this.unitOfwork.PriceList.Get();
            return View(items);
        }

        // GET: PriceList/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = this.unitOfwork.PriceList.GetByID(id);

           
            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        // GET: PriceList/Create
        public ActionResult Create()
        {
            this.PopulateCustomersDropDown();
            this.PopulateProductsDropDown();
            return View();
        }

        // POST: PriceList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustomerId","ProductId","Price")] PriceListVM priceList)
        {
            if (ModelState.IsValid)
            {
                var var_customer = this.unitOfwork.Customer.GetByID(priceList.CustomerId);
                var var_product = this.unitOfwork.Product.GetByID(priceList.ProductId);

                this.unitOfwork.PriceList.Insert(new PriceList
                {
                    Id = priceList.Id,
                    Product = var_product,
                    Price = priceList.Price,
                    Customer = var_customer
                });
                this.unitOfwork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(priceList);
        }

        private void PopulateCustomersDropDown()
        {
            var customers = this.unitOfwork.Customer.Get();
            ViewBag.CustomerList = new SelectList(customers, "Id", "Name");
        }

        private void PopulateProductsDropDown()
        {
            var products = this.unitOfwork.Product.Get();
            ViewBag.ProductList = new SelectList(products, "Id", "Name");
        }
    }

}