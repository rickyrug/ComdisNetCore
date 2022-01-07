using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Comdis.Models
{
    public class UOM: AuditFields
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Bank")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Code")]
        public string Code { get; set; }
        //public ICollection<Product> Product { get; set; }

        public UOM() {}
    }
}
