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

        public IQueryable<Customer> GetByPatern(string patern)
        {
                  var list = _context.Customer.Where(c => c.Name.Contains(patern)).AsQueryable();
                  return list;
        }
    }
}

