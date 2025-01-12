using System;
namespace DataAccess.Models
{
	public class PriceList: AuditFields
	{
		public PriceList()
		{
			
		}

        public int Id { get; set; }
        public Product Product { get; set; }
        public Double Price { get; set; }
		public Customer? Customer { get; set; }
    }
}

