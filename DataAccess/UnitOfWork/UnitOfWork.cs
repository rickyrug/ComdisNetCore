using System;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UnitOfWork
{
	public class UnitOfWork: IDisposable
	{

		private ComdisContext _context;
        private IBank _bank;
        private IConfiguration _configuration;
        private ICustomer _customer;

		public UnitOfWork(ComdisContext context)
		{
			this._context = context;
		}

        public ICustomer Customer
        {
            get
            {
                if (_customer == null)
                    _customer = new CustomerRepository(this._context);
                return _customer;
            }
        }

        public IConfiguration Configuration
        {
            get
            {
                if(_configuration == null)
                {
                    _configuration = new ConfigurationRepository(this._context);
                }
                return _configuration;
            }
        }

        public IBank Bank {
            get
            {
                if(_bank == null)
                {
                    _bank = new BankRepository(this._context);
                }
                return _bank;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

