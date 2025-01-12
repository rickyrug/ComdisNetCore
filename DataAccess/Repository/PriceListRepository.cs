using System;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class PriceListRepository:GenericRepository<PriceList>,IPriceList
	{
		public PriceListRepository(ComdisContext context) : base(context)
        {
		}

		public override IEnumerable<PriceList> Get()
		{
			var list = _context.PriceList
				.Include(p => p.Product)
				.Include(c => c.Customer)
				.ToList();

			return list;
		}

		public override	PriceList GetByID(object id)
		{
			var item = _context.PriceList
				.Include(p => p.Product)
				.Include(c => c.Customer)
				.FirstOrDefault(x => x.Id == (int)id);

			return item;
		}


	}
}

