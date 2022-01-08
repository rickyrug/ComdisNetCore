using System;
using System.ComponentModel.DataAnnotations;

namespace Comdis.Models.VM
{
    public class SalesItemVM:AuditFields
    {


        public int uid { get; set; }
        
        public int SOId { get; set; }

        [Required]
        [Range(1,999999,ErrorMessageResourceType = typeof(Resources.Resources),ErrorMessageResourceName = "MSG_RequieredProduct")]
        [Display(ResourceType =typeof(Resources.Resources),Name = "Label_Product")]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Quantity")]
        public decimal Quantity { get; set; }

        [Required]
      //  [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessageResourceType = typeof(Resources.Resources),ErrorMessageResourceName = "Label_2Decimals")]
        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Price")]
        public decimal Price { get; set; }

        public SalesItemVM(){}
    }
}
