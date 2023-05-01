using System;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repository
{
	public class CustomerRepository : GenericRepository<Customer>,ICustomer
    {
		public CustomerRepository(ComdisContext context):base(context)
		{
		}

        public IList<Customer> GetByPatern(string patern)
        {
                  var list = _context.Customer.Where(c => c.Name.Contains(patern)).ToList();
                    return list;
        }
    }
}

