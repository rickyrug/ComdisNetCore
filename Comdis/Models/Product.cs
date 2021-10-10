using System;
using System.Collections;
using System.Collections.Generic;

namespace Comdis.Models
{
    public class Product : AuditFields
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public UOM Uom { get; set; }
        public ProductCategory category {get; set;}
        public string Code { get; set; }


        public Product() { }
    }
}
