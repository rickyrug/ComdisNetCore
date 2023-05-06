using System;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repository
{
	public class PurchaseRepository:GenericRepository<Purchase>,IPurchase
	{
		public PurchaseRepository(ComdisContext context):base(context)
		{
		}
	}
}

