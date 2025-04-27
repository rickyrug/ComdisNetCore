using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comdis.Models.VM;
using DataAccess.Models;
using Comdis.DataAccess.UnitOfWork;

namespace Comdis.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerController(IUnitOfWork punitOfWork)
        {
            this.unitOfWork = punitOfWork;
        }

        // GET: Customer
        public IActionResult Index()
        {
            var items = unitOfWork.Customer.Get();
            return View(items);
        }

        // GET: Customer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = this.unitOfWork.Customer.GetByID(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Adress,Adress2,Phone1,Phone2,RFC,email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CreatedBy = "";
                customer.Cretead   = DateTime.Now;
                customer.Updated   = DateTime.Now;
                customer.UpdatedBy = "";

                this.unitOfWork.Customer.Insert(customer);

                this.unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = this.unitOfWork.Customer.GetByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Adress,Adress2,Phone1,Phone2,RFC,email,CreatedBy,Cretead")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     customer.Updated   = DateTime.Now;
                        customer.UpdatedBy = "";

                    this.unitOfWork.Customer.Update(customer);
                    this.unitOfWork.Save();
             
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = this.unitOfWork.Customer.GetByID(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = this.unitOfWork.Customer.GetByID(id);
            this.unitOfWork.Customer.Delete(customer);
            this.unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        public JsonResult GetCustomerList(QuerySelect pParam)
        {
            List<SelectObject> selectObj = new List<SelectObject>();
            var customerList = this.unitOfWork.Customer.GetByPatern(pParam.search);

            foreach (var customer in customerList)
            {
                selectObj.Add(new SelectObject()
                {
                    id = customer.Id
                    ,
                    text = customer.Name
                });
            }

            return Json(selectObj);

        }

        private bool CustomerExists(int id)
        {
            return this.unitOfWork.Customer.Exist(id);
        }
    }
}
