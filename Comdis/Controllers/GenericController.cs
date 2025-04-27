using System;
using System.Collections.Generic;
using System.Linq;
using Comdis.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Comdis.Comdis.Controllers
{

    public class GenericController: Controller
    {
       public readonly IUnitOfWork unitOfWork;
        public GenericController(IUnitOfWork punitOfWork)
        {
            this.unitOfWork = punitOfWork;
        }

        public ActionResult Index<T>(IQueryable<T> items) where T : class
        {
            return View(items.ToList());
        }

        public ActionResult Details<T>(int? id) where T : class
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = unitOfWork.GetRepository<T>().GetByID(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public ActionResult Create<T>() where T : class
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create<T>(T item) where T : class
        {
            if (ModelState.IsValid)
            {
                item.GetType().GetProperty("Cretead").SetValue(item, DateTime.Now);
                item.GetType().GetProperty("CreatedBy").SetValue(item, User.Identity.Name);
                item.GetType().GetProperty("Updated").SetValue(item, DateTime.Now);
                item.GetType().GetProperty("UpdatedBy").SetValue(item, User.Identity.Name);
                
                unitOfWork.GetRepository<T>().Insert(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public ActionResult Edit<T>(int? id) where T : class
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = unitOfWork.GetRepository<T>().GetByID(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit<T>(T item) where T : class
        {
            if (ModelState.IsValid)
            {
                item.GetType().GetProperty("ModifiedDate").SetValue(item, DateTime.Now);
                item.GetType().GetProperty("ModifiedBy").SetValue(item, User.Identity.Name);
                unitOfWork.GetRepository<T>().Update(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public ActionResult Delete<T>(int? id) where T : class
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = unitOfWork.GetRepository<T>().GetByID(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed<T>(int id) where T : class
        {
            var item = unitOfWork.GetRepository<T>().GetByID(id);
            unitOfWork.GetRepository<T>().Delete(item);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        private bool ItemExists<T>(int id) where T : class
        {
            return unitOfWork.GetRepository<T>().GetByID(id) != null;
        }
        private bool ItemExists<T>(string id) where T : class
        {
            return unitOfWork.GetRepository<T>().GetByID(id) != null;
        }
        private bool ItemExists<T>(Guid id) where T : class
        {
            return unitOfWork.GetRepository<T>().GetByID(id) != null;
        }
        private bool ItemExists<T>(long id) where T : class
        {
            return unitOfWork.GetRepository<T>().GetByID(id) != null;
            }
    }


}