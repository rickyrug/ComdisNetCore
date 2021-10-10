using System;
using System.ComponentModel.DataAnnotations;

namespace Comdis.Models.VM
{
    public class ProductVM
    {
        public ProductVM()
        {
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public int? UomId { get; set; }
        
        public int? categoryId { get; set; }
        public string Code { get; set; }
    }
}
