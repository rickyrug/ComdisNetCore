using System;
using System.Collections.Generic;
using System.Linq;

namespace Comdis.Models.VM
{

    public class PriceListVM
    {
        
        public PriceListVM(){}
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Double Price { get; set; }
		public int CustomerId { get; set; }
            
        }

}