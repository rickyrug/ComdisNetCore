using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Comdis.Models
{
    public class ProductCategory: AuditFields
    {
        public ProductCategory()
        {
        }

        public int Id { get; set; }
        [DisplayName("Category")]
        public string Name { get; set; }
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
