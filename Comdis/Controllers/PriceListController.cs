using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Models.VM;
using DataAccess.Models;
using Comdis.DataAccess.UnitOfWork;
using Comdis.Comdis.Controllers;
using System.Linq;


namespace Comdis.Controllers
{
    public class PriceListController : GenericController
    {
        public PriceListController(IUnitOfWork punitOfWork) : base(punitOfWork)
        {
        }

        // GET: PriceList
        public  ActionResult Index()
        {
           var items =  this.unitOfWork.PriceList.Get().Include(p => p.Customer).Include(p => p.Product).ToList();
            return View(items);
        }

        // GET: PriceList/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = this.unitOfWork.PriceList.GetByID(id);

           
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
                try
                {

                
                var var_customer = this.unitOfWork.Customer.GetByID(priceList.CustomerId);
                var var_product = this.unitOfWork.Product.GetByID(priceList.ProductId);

                this.unitOfWork.PriceList.Insert(new PriceList
                {
                    Id = priceList.Id,
                    Product = var_product,
                    Price = priceList.Price,
                    Customer = var_customer,
                    Cretead = DateTime.Now,
                    Updated = DateTime.Now
                
                });
                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE constraint failed"))
                    {
                        ModelState.AddModelError("", "Ya existe un registro con el mismo Producto y Cliente.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo guardar los cambios. Inténtalo de nuevo, y si el problema persiste, contacta al administrador del sistema.");
                    }
                    // Puedes registrar el error para depuración
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                     // Maneja cualquier otra excepción
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Inténtalo de nuevo, y si el problema persiste, contacta al administrador del sistema.");
                    // Puedes registrar el error para depuración
                    Console.WriteLine(ex.Message);
                } 
            }
            this.PopulateCustomersDropDown(priceList.CustomerId);
            this.PopulateProductsDropDown(priceList.ProductId);
            return View(priceList);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = this.unitOfWork.PriceList.Get(a => a.Id == id).Include(p => p.Customer).Include(p => p.Product).FirstOrDefault();
            if (priceList == null)
            {
                return NotFound();
            }
            
            
            if (priceList == null)
            {
                return NotFound();
            }
            this.PopulateCustomersDropDown(priceList.Customer.Id);
            this.PopulateProductsDropDown(priceList.Product.Id);
            
            var priceListVM = new PriceListVM
            {
                Id = priceList.Id,
                CustomerId = priceList.Customer.Id,
                ProductId = priceList.Product.Id,
                Price = priceList.Price
            };

            return View(priceListVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CustomerId,ProductId,Price")] PriceListVM priceList)
        {
            if (id != priceList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var var_customer = this.unitOfWork.Customer.GetByID(priceList.CustomerId);
                var var_product = this.unitOfWork.Product.GetByID(priceList.ProductId);

                var priceListEntity = this.unitOfWork.PriceList.GetByID(id);
                priceListEntity.Product = var_product;
                priceListEntity.Price = priceList.Price;
                priceListEntity.Customer = var_customer;
                priceListEntity.Updated = DateTime.Now;

                this.unitOfWork.PriceList.Update(priceListEntity);
                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(priceList);
        }

        // GET: PriceList/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = this.unitOfWork.PriceList.GetByID(id);
            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        // POST: PriceList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var priceList = this.unitOfWork.PriceList.GetByID(id);
            this.unitOfWork.PriceList.Delete(priceList);
            this.unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateCustomersDropDown(object selectedCustomer = null)
        {
            var customers = this.unitOfWork.Customer.Get();
            ViewBag.CustomerList = new SelectList(customers, "Id", "Name", selectedCustomer);
        }

        private void PopulateProductsDropDown(object selectedProduct = null)
        {
            var products = this.unitOfWork.Product.Get();
            ViewBag.ProductList = new SelectList(products, "Id", "Name", selectedProduct);
        }
    }

}