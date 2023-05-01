using System;
namespace Comdis.Models
{
	public class PurchaseItems
	{

		public int Id { get; set; }
		public Purchase POHeader { get; set; }
		public Product Product { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }

		public PurchaseItems()
		{
		}
	}
}

