using System;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repository
{
	public class ProductCategoryRepository:GenericRepository<ProductCategory>,IProductCategory
	{
		public ProductCategoryRepository(ComdisContext context):base(context)
		{
		}
	}
}

