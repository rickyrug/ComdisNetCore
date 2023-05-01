using System;
using DataAccess.Models;

namespace DataAccess.Interfaces
{
	public interface ICustomer:IGenericRepository<Customer>
	{
		IList<Customer> GetByPatern(string patern);
	}
}

