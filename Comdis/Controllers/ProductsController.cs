using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Comdis.Models.VM;
using DataAccess.UnitOfWork;
using DataAccess.Models;

namespace Comdis.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public ProductsController(ComdisContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
           
        }

        // GET: Products
        public IActionResult Index()
        {
            var items = this.unitOfWork.Product.Get();
              return View(items);
           // return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.unitOfWork.Product.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
          
            PopulateUomDropDown();

            PopulateProdCatDropDown();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,UomId,categoryId,Code")] ProductVM product)
        {
            if (ModelState.IsValid)
            {

               
                Product newproduct = new Product();
                var var_uom = this.unitOfWork.Uom.GetByID(product.UomId);
                var var_cat = this.unitOfWork.ProductCategory.GetByID(product.categoryId);

              

                newproduct.Name = product.Name;
                newproduct.Code = product.Code;
                newproduct.Uom = var_uom;
                newproduct.category = var_cat;
                //newproduct.category = _context.ProductCategory
                //                                                 .AsNoTrackingWithIdentityResolution()
                //                                                    .FirstOrDefault(pc => pc.Id == product.categoryId);


                newproduct.Cretead = DateTime.Now;
                newproduct.CreatedBy = "";
                newproduct.Updated = DateTime.Now;
                newproduct.UpdatedBy = "";

                this.unitOfWork.Product.Insert(newproduct);
                this.unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.unitOfWork.Product.GetByID(id);
            
            if (product == null)
            {
                return NotFound();
            }
            if(product.Uom == null)
            {
                PopulateUomDropDown();
            }
            else
            {
                PopulateUomDropDown(product.Uom.Id);
            }

            if(product.category == null)
            {
                PopulateProdCatDropDown();
            }
            else
            {
                PopulateProdCatDropDown(product.category.Id);
            }

            ProductVM productVM = new ProductVM();
            productVM.Name              = product.Name;
            productVM.Id                    = product.Id;
            productVM.UomId             = product.Uom.Id;
            productVM.categoryId = product.category.Id;
            productVM.Code                  = product.Code;
            
            return View(productVM);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,UomId,categoryId,Code")] ProductVM product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    Product editedProduct = this.unitOfWork.Product.GetByID(id);


                    editedProduct.Name = product.Name;
                    editedProduct.Code = product.Code;

                    editedProduct.Uom = this.unitOfWork.Uom.GetByID(product.UomId);//_context.UOM.Find(product.UomId);
                    editedProduct.category = this.unitOfWork.ProductCategory.GetByID(product.categoryId);//_context.ProductCategory.Find( product.categoryId);

                    editedProduct.Updated = DateTime.Now;
                    editedProduct.UpdatedBy = "";

                    this.unitOfWork.Product.Update(editedProduct);
                    this.unitOfWork.Save();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.unitOfWork.Product.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = this.unitOfWork.Product.GetByID(id);
            this.unitOfWork.Product.Delete(product);
            this.unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

       public JsonResult GetProducts(QuerySelect pParam)
        {

            List<SelectObject> selectObj = new List<SelectObject>();
            var productList = this.unitOfWork.Product.Get();


            

            foreach(var product in productList)
            {
                selectObj.Add(new SelectObject() {
                     id = product.Id
                    ,text = product.Name
                });
            }

            return Json(selectObj);
        }

        private bool ProductExists(int id)
        {
            return this.unitOfWork.Product.Exist(id);
        }

        private void PopulateUomDropDown(object selectedUom = null)
        {
            var uomList =  this.unitOfWork.Uom.Get(); 
            ViewBag.uomList = new SelectList(uomList,"Id","Name" ,selectedUom);

        }

        private void PopulateProdCatDropDown(object selectedItem = null)
        {
            var prodCatList = this.unitOfWork.ProductCategory.Get();
            ViewBag.pcList = new SelectList(prodCatList, "Id", "Name", selectedItem);

        }

    }
}
