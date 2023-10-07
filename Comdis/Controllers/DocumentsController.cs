using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comdis.BusinessRules;
using Comdis.Models.VM;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comdis.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public DocumentsController(ComdisContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetInvoice(int pInvoiceId)
        {
            SalesOrdersBusinessRules salesOrdersBusinessRules = new SalesOrdersBusinessRules(this.unitOfWork);
            SalesVM sales =  salesOrdersBusinessRules.GetSalesOrderItemDetails(pInvoiceId);
            return PartialView("Invoice",sales);
        }
    }
}

