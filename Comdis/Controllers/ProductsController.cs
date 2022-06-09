using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comdis.Models;
using Comdis.Models.VM;

namespace Comdis.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ComdisContext _context;

        public ProductsController(ComdisContext context)
        {
           // context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
           
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return View(await _context.Product
                            
                                .Include(u => u.Uom)
                              
                                .Include(pc => pc.category)
                                .AsNoTracking()
                                .ToListAsync());
           // return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
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
                var var_uom = _context.UOM.Find(product.UomId);
                var var_cat = _context.ProductCategory.Find(product.categoryId);

              

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


                _context.Product.Add(newproduct);
                
                 _context.SaveChanges();
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

            var product = _context.Product.
                                    Include(u => u.Uom).
                                    Include(pc => pc.category).
                                    AsNoTracking().
                                    FirstOrDefault(p => p.Id == id);
            
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UomId,categoryId,Code")] ProductVM product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    Product editedProduct = _context.Product.Where(p => p.Id == product.Id).FirstOrDefault();


                    editedProduct.Name = product.Name;
                    editedProduct.Code = product.Code;

                    editedProduct.Uom       = _context.UOM.Find(product.UomId);
                    editedProduct.category = _context.ProductCategory.Find( product.categoryId);

                    editedProduct.Updated = DateTime.Now;
                    editedProduct.UpdatedBy = "";


                    _context.Product.Update(editedProduct);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       public JsonResult GetProducts(QuerySelect pParam)
        {

            List<SelectObject> selectObj = new List<SelectObject>();
            var productList = _context.Product
                                            .Include(u => u.Uom)
                                            .Include(c => c.category)
                                            .Where(p=> p.Name.Contains(pParam.search))
                                            .AsNoTracking()
                                            .ToList();


            

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
            return _context.Product.Any(e => e.Id == id);
        }

        private void PopulateUomDropDown(object selectedUom = null)
        {
            var uomList =  _context.UOM.AsNoTracking().ToList(); 
            ViewBag.uomList = new SelectList(uomList,"Id","Name" ,selectedUom);

        }

        private void PopulateProdCatDropDown(object selectedItem = null)
        {
            var prodCatList = _context.ProductCategory.AsNoTracking().ToList();
            ViewBag.pcList = new SelectList(prodCatList, "Id", "Name", selectedItem);

        }

    }
}
