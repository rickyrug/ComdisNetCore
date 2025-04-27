using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class UOM : AuditFields
    {
        public UOM() {}
        public int Id { get; set; }

       [Display(ResourceType = typeof(Resources.Resources), Name = "Label_UOM")]
        public  string Name { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Code")]
        public  string Code { get; set; }
        //public ICollection<Product> Product { get; set; }

        
    }
}
