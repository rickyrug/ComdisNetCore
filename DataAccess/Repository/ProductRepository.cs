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

    }
}

