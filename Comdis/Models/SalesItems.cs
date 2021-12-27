using System;
namespace Comdis.Models
{
    public class SalesItems:AuditFields
    {
        public int Id { get; set; }
        public Sales SalesHeader { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public SalesItems()
        {
        }
    }
}
