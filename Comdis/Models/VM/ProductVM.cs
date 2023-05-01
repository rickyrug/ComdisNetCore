using System;
using System.ComponentModel.DataAnnotations;
using DataAccess.Resources;

namespace Comdis.Models.VM
{
    public class ProductVM
    {
        public ProductVM()
        {
        }


        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "Label_Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "Label_UOM")]
        public int? UomId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "Label_Category")]
        public int? categoryId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "Label_Code")]
        public string Code { get; set; }
    }
}
