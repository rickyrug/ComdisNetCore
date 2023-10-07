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
using DataAccess.UnitOfWork;
using DataAccess.Models;
using DataAccess.Resources;
using Comdis.BusinessRules;

namespace Comdis.Controllers
{
    public class SalesItemsController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public SalesItemsController(ComdisContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        // GET: SalesItems
        public IActionResult Index(int id)
        {
            Sales sales = this.unitOfWork.Sales.GetByID(id);
                               

            ViewBag.So = id.ToString();
            sales.discount  = sales.discount * 100;
            sales.discount2 = sales.discount2 * 100;
            sales.discount3 = sales.discount3 * 100;
            sales.tax = sales.tax * 100;
            return View(sales);
        }

        // GET: SalesItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesItems = this.unitOfWork.Sales.GetByID(id);
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
                    newItem.SalesHeader = this.unitOfWork.Sales.GetByID(salesItems.SOId); 
                    newItem.Product = this.unitOfWork.Product.GetByID(salesItems.ProductId); 


                    newItem.Price = salesItems.Price;
                    newItem.Quantity = salesItems.Quantity;
                    newItem.Cretead = DateTime.Now;

                    this.unitOfWork.SalesItems.Insert(newItem);
                    this.unitOfWork.Save();
                    

                    salesItems.uid = newItem.Id;
                    salesItems.ProductName = newItem.Product.Name;
                    salesItems.ProductId = newItem.Product.Id;

                    return Json(new MessageVM<SalesItemVM>()
                            {
                                    hasError = false
                                , Message = salesItems
                                ,shortMessage = String.Format(Resources.Msg_addedItem,salesItems.ProductName)


                    });
                }


                var validation = ModelState.ToList();
                List<FormField> errorsInForm = new List<FormField>();
                errorsInForm = FormValidationHelper.processErrorsInForm(validation);
                return Json(new MessageVM<List<FormField>>()
                    {
                            hasError = true
                        , Message = errorsInForm
                        
                    }


                );

               

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {

                    throw new Exception(String.Format(Resources.MSG_ErrorCreating, salesItems.ProductName + " " + ex.InnerException));
                    
                }
                else
                {
                    throw new Exception(String.Format(Resources.MSG_ErrorCreating, salesItems.ProductName + " " + ex.Message));
              
                }  
                
            }
            
        }

        // GET: SalesItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesItems = this.unitOfWork.SalesItems.GetByID(id);//await _context.SalesItems.FindAsync(id);
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
        public JsonResult Edit([Bind("Quantity,Price,SOId,ProductId,uid")] SalesItemVM salesItems)
        {

          
            if (ModelState.IsValid)
            {
                try
                {
                    SalesItems editSalesItem = this.unitOfWork.SalesItems.GetByID(salesItems.uid);// _context.SalesItems.Find(salesItems.uid);
                    editSalesItem.SalesHeader = this.unitOfWork.Sales.GetByID(salesItems.SOId);//_context.Sales.Find(salesItems.SOId);
                    editSalesItem.Product = this.unitOfWork.Product.GetByID(salesItems.ProductId);//_context.Product.Find(salesItems.ProductId);
                    editSalesItem.Price = salesItems.Price;
                    editSalesItem.Quantity = salesItems.Quantity;

                    editSalesItem.Updated = DateTime.Now;

                    this.unitOfWork.SalesItems.Update(editSalesItem);
                    this.unitOfWork.Save();
                   

                    salesItems.ProductName = editSalesItem.Product.Name;

                    return Json(new MessageVM<SalesItemVM>()
                    {
                        hasError = false
                                ,
                        Message = salesItems
                                ,
                        shortMessage = String.Format(Resources.MSG_EditedITem, salesItems.ProductName)


                    });
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        throw new Exception( String.Format(Resources.MSG_ErrorEditar,salesItems.ProductName + " "+ ex.InnerException));
                    }
                    else
                    {
                        throw new Exception(String.Format(Resources.MSG_ErrorEditar, salesItems.ProductName + " " + ex.Message));
                        
                    }
                }
                
            }
            var validation = ModelState.ToList();
            List<FormField> errorsInForm = new List<FormField>();
            errorsInForm = FormValidationHelper.processErrorsInForm(validation);


            return Json(new MessageVM<List<FormField>>()
            {
                hasError = true
                    ,
                Message = errorsInForm

            });
        }

        // GET: SalesItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesItems = this.unitOfWork.SalesItems.GetByID(id);//await _context.SalesItems
                
            if (salesItems == null)
            {
                return NotFound();
            }

            return View(salesItems);
        }

        // POST: SalesItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var salesItems = this.unitOfWork.SalesItems.GetByID(id); //await _context.SalesItems.FindAsync(id);
            this.unitOfWork.SalesItems.Delete(salesItems);
            this.unitOfWork.Save();
           
            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetCreateItemView(int pSOI)
        {
            SalesItemVM soi = new SalesItemVM();

            soi.SOId = pSOI;

            return PartialView("_CreateSOI", soi);
        }

        public IActionResult GetEditItemView(SalesItemVM pSOIDetail)
        {


            return PartialView("_EditSOI", pSOIDetail);
        }

        

        [HttpPost]
        public JsonResult GetSalesOrderItem(int id)
        {
          
            try
            {
                SalesOrdersBusinessRules salesOrdersBusinessRules = new SalesOrdersBusinessRules(this.unitOfWork);
                return Json(salesOrdersBusinessRules.GetSalesOrderItemDetails(id));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult deleteItem(int pId)
        {
            try
            {
                SalesItems deleteItem = this.unitOfWork.SalesItems.GetByID(pId);

                 



                SalesItemVM salesItems = new SalesItemVM()
                {
                    uid = deleteItem.Id,
                    ProductName = deleteItem.Product.Name

                };

                this.unitOfWork.SalesItems.Delete(deleteItem);
                this.unitOfWork.Save();
                

                return Json(new MessageVM<SalesItemVM>()
                {
                    hasError = false
                                ,
                    Message = salesItems
                                ,
                    shortMessage = String.Format(Resources.MSG_DeletedItem, salesItems.ProductName)


                });

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }

        private bool SalesItemsExists(int id)
        {
            return this.unitOfWork.SalesItems.Exist(id);
        }
    }
}
