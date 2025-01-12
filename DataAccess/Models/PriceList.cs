using System;
using System.ComponentModel.DataAnnotations;
using DataAccess.Resources;
namespace DataAccess.Models
{
	public class PriceList: AuditFields
	{
		public PriceList()
		{
			
		}

        public int Id { get; set; }

		[Display(ResourceType = typeof(Resources.Resources), Name = "Label_Product")]
        public Product Product { get; set; }
		[Display(ResourceType = typeof(Resources.Resources), Name = "Label_Price")]
        public Double Price { get; set; }
		[Display(ResourceType = typeof(Resources.Resources), Name = "Label_Customer")]
		public Customer? Customer { get; set; }
    }
}

