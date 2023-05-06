using System;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class SalesRepository:GenericRepository<Sales>,ISales
	{
		public SalesRepository(ComdisContext context):base(context)
		{
		}

		public override IEnumerable<Sales> Get()
		{
			var list = _context.Sales.
											Include(sales => sales.SalesToParty)
										 .OrderByDescending(x => x.Id)
										 .Take(50)
										 .ToList();
			return list;
        }

		public override Sales GetByID(object id)
		{
			var item = _context.Sales
									
                                .Include(soi => soi.SalesItems)
                                .ThenInclude(soi => soi.Product)
                                .Include(cu => cu.SalesToParty)
							    .FirstOrDefault(header => header.Id == (int)id);
			return item;

        }
    }
}

