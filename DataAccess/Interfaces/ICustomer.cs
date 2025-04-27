using System;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
	public interface ICustomer:IGenericRepository<Customer>
	{
		IQueryable<Customer> GetByPatern(string patern);
	}
}

