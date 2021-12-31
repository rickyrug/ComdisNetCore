using System;
namespace Comdis.Models.VM
{
    public class SalesItemVM:AuditFields
    {


        public int uid { get; set; }
        public int SOId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public SalesItemVM(){}
    }
}
