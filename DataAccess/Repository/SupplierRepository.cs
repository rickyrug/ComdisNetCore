using System;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class SupplierRepository:GenericRepository<Supplier>,ISupplier
	{
		public SupplierRepository(ComdisContext context):base(context)
		{
		}

		public override Supplier GetByID(object id)
		{
			var item = _context.Supplier
				  .Include(s => s.SuscribedBank)
				  .AsNoTracking()
			  .FirstOrDefault(m => m.Id == (int)id);

			return item;

        }
    }
}

