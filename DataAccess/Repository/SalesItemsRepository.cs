using System;
using System.Security.Cryptography;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class SalesItemsRepository:GenericRepository<SalesItems>,ISalesItem
	{
		public SalesItemsRepository(ComdisContext context):base(context)
		{
		}

		public override SalesItems GetByID(object id)
		{
				var item =								_context.SalesItems
																	.Include(p => p.Product)
                                                                .Where(item => item.Id == (int)id)
																.FirstOrDefault();
			return item;
        }

    }
}

