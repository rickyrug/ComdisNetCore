using System;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repository
{
	public class UOMRepository:GenericRepository<UOM>, IUom
	{
		public UOMRepository(ComdisContext context):base(context)
		{
		}
	}
}

