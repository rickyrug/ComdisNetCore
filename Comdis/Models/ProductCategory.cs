using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Comdis.Models
{
    public class ProductCategory: AuditFields
    {
        public ProductCategory()
        {
        }

        public int Id { get; set; }


        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Code")]
        public string CategoryCode { get; set; }
        //public ICollection<Product> Product { get; set; }





        public string setProductCode(int pIdProduct, string pCategoryCode)
        {
            string var_productcodeFormat = @"{0}{1}"; // Category code + product number pad 5 0

            string var_productCode = String.Format(var_productcodeFormat, pCategoryCode, pIdProduct.ToString().PadLeft(3, '0'));

            return var_productCode;
        }

    }
}
