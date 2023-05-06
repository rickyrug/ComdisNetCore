using System;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class ProductRepository: GenericRepository<Product>, IProduct
	{
		public ProductRepository(ComdisContext context):base(context)
		{
		}

		public override IEnumerable<Product> Get()
		{
         var list =    _context.Product

                    .Include(u => u.Uom)
                    .Include(pc => pc.category)
                   // .AsNoTracking()
                    .Take(50)
                    .ToList();

            return list;

        }

        public override Product GetByID(object id)
        {
          var product =  _context.Product

                    .Include(u => u.Uom)
                    .Include(pc => pc.category)
                    //.AsNoTracking()
                    .Where(p => p.Id == (int)id)
                    .FirstOrDefault();

            return product;
        }
    }
}

