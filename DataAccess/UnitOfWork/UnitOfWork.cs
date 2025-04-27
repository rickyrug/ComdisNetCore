using System;
using Comdis.DataAccess.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{

        private readonly ComdisContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        public UnitOfWork(ComdisContext context)
        {
            _context = context;
        }

        private IGenericRepository<Bank> _bank;
        public IGenericRepository<Bank> Bank => _bank ??= new GenericRepository<Bank>(_context);

        private ICustomer _customer;
        public ICustomer Customer => _customer ??= new CustomerRepository(_context);

        private IGenericRepository<Supplier> _supplier;
        public IGenericRepository<Supplier> Supplier => _supplier ??= new GenericRepository<Supplier>(_context);

        private IGenericRepository<ProductCategory> _productCategory;
        public IGenericRepository<ProductCategory> ProductCategory => _productCategory ??= new GenericRepository<ProductCategory>(_context);

        private IGenericRepository<Product> _product;
        public IGenericRepository<Product> Product => _product ??= new GenericRepository<Product>(_context);

        private IGenericRepository<UOM> _uom;
        public IGenericRepository<UOM> Uom => _uom ??= new GenericRepository<UOM>(_context);

        private IGenericRepository<Purchase> _purchase;
        public IGenericRepository<Purchase> Purchase => _purchase ??= new GenericRepository<Purchase>(_context);

        private IGenericRepository<Sales> _sales;
        public IGenericRepository<Sales> Sales => _sales ??= new GenericRepository<Sales>(_context);

        private IGenericRepository<SalesItems> _salesItems;
        public IGenericRepository<SalesItems> SalesItems => _salesItems ??= new GenericRepository<SalesItems>(_context);

        private IGenericRepository<PriceList> _priceList;
        public IGenericRepository<PriceList> PriceList => _priceList ??= new GenericRepository<PriceList>(_context);

        private IGenericRepository<Configuration> _configuration;
        public IGenericRepository<Configuration> Configuration => _configuration ??= new GenericRepository<Configuration>(_context);
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return (IGenericRepository<T>)_repositories.GetOrAdd(typeof(T), _ => new GenericRepository<T>(_context));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}

