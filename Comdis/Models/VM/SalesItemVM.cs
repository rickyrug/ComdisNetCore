﻿using System;
namespace Comdis.Models.VM
{
    public class SalesItemVM:AuditFields
    {


        
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public SalesItemVM(){}
    }
}
