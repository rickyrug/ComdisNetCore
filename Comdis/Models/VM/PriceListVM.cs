using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataAccess.Resources;

namespace Comdis.Models.VM
{

    public class PriceListVM
    {
        
        public PriceListVM(){}
        public int Id { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "Label_Product")]
        public int ProductId { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "Label_Price")]
        public Double Price { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "Label_Customer")]
		public int CustomerId { get; set; }
            
        }

}