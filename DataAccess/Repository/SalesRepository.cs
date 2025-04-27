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
		

		
    }
}

