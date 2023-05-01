using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Comdis.Models
{
    public class Product : AuditFields
    {

        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_UOM")]
        public UOM Uom { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Category")]
        public ProductCategory category {get; set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Code")]
        public string Code { get; set; }


        public Product() { }
    }
}
