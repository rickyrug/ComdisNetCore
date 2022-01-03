using System;
using System.ComponentModel.DataAnnotations;

namespace Comdis.Models.VM
{
    public class SalesItemVM:AuditFields
    {


        public int uid { get; set; }
        
        public int SOId { get; set; }
        [Required]
        [Range(1,999999)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal Price { get; set; }

        public SalesItemVM(){}
    }
}
