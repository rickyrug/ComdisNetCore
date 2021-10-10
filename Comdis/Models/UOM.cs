using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Comdis.Models
{
    public class UOM: AuditFields
    {
        public int Id { get; set; }

        [DisplayName("UOM")]
        public string Name { get; set; }
        public string Code { get; set; }
        //public ICollection<Product> Product { get; set; }

        public UOM() {}
    }
}
