using Comdis.BusinessRules;
using Comdis.Comdis.Controllers;
using Comdis.DataAccess.UnitOfWork;
using Comdis.Models.VM;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comdis.Controllers
{
    public class DocumentsController : GenericController
    {
        public DocumentsController(IUnitOfWork punitOfWork) : base(punitOfWork)
        {
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

