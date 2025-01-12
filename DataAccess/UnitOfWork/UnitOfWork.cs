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
        private ISupplier _supplier;
        private IProductCategory _productCategory;
        private IProduct _product;
        private IUom _uom;
        private IPurchase _purchase;
        private ISales _sales;
        private ISalesItem _salesItems;
        private IPriceList _PriceList;

		public UnitOfWork(ComdisContext context)
		{
			this._context = context;
		}

        public IPriceList PriceList
        {
            get
            {
                if (_PriceList == null)
                    _PriceList = new PriceListRepository(this._context);
                return _PriceList;
            }
        }

        public ISalesItem SalesItems
        {
            get
            {
                if (_salesItems == null)
                    _salesItems = new SalesItemsRepository(this._context);
                return _salesItems;
            }
        }

        public ISales Sales
        {
            get
            {
                if (_sales == null)
                    _sales = new SalesRepository(this._context);
                return _sales;
            }
        }

        public IPurchase Purchase
        {
            get
            {
                if (_purchase == null)
                    _purchase = new PurchaseRepository(this._context);
                return _purchase;
            }
        }

        public IUom Uom
        {
            get
            {
                if (_uom == null)
                    _uom = new UOMRepository(this._context);
                return _uom;
            }
        }

        public IProduct Product
        {

            get
            {
                if (_product == null)
                    _product = new ProductRepository(this._context);
                return _product;
            }
        }

        public IProductCategory ProductCategory
        {
            get
            {
                if (_productCategory == null)
                    _productCategory = new ProductCategoryRepository(this._context);
                return _productCategory;
            }
        }

        public ISupplier Supplier
        {
            get
            {
                if (_supplier == null)
                    _supplier = new SupplierRepository(this._context);
                return _supplier;
            }
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

