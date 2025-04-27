using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Comdis.Comdis.Controllers;
using Comdis.DataAccess.UnitOfWork;

namespace Comdis.Controllers
{
    public class ProductCategoryController : GenericController
    {
        public ProductCategoryController(IUnitOfWork punitOfWork) : base(punitOfWork)
        {
        }

        // GET: ProductCategory
        public IActionResult Index()
        {
            var items = this.unitOfWork.ProductCategory.Get();
            return View(items);
        }

        // GET: ProductCategory/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = this.unitOfWork.ProductCategory.GetByID(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,CategoryCode")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.CreatedBy = "";
                productCategory.Cretead = DateTime.Now;
                productCategory.Updated = DateTime.Now;
                productCategory.UpdatedBy = "";

                this.unitOfWork.ProductCategory.Insert(productCategory);
                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = this.unitOfWork.ProductCategory.GetByID(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,CategoryCode")] ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductCategory editedProductCategory = this.unitOfWork.ProductCategory.GetByID(id);

                    editedProductCategory.Name =              productCategory.Name;
                    editedProductCategory.CategoryCode = productCategory.CategoryCode;
                    editedProductCategory.Updated     = DateTime.Now;
                    editedProductCategory.UpdatedBy = "";


                    this.unitOfWork.ProductCategory.Update(editedProductCategory);
                    this.unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.Id))
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
            return View(productCategory);
        }

        // GET: ProductCategory/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = this.unitOfWork.ProductCategory.GetByID(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productCategory = this.unitOfWork.ProductCategory.GetByID(id);
            this.unitOfWork.ProductCategory.Delete(productCategory);
            this.unitOfWork.Save();
      
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return this.unitOfWork.ProductCategory.Exist(id);
        }
    }
}
