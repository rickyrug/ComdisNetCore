using System;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repository
{
	public class BankRepository: GenericRepository<Bank>, IBank
	{
		public BankRepository(ComdisContext context):base(context)
		{
		}
	}
}

