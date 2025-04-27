using System;
using System.Collections.Generic;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace Comdis.DataAccess.UnitOfWork
{

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<PriceList> PriceList { get; }
        IGenericRepository<SalesItems> SalesItems { get; }
        IGenericRepository<Sales> Sales { get; }
        IGenericRepository<Purchase> Purchase { get; }
        IGenericRepository<UOM> Uom { get; }
        ICustomer  Customer { get; }
        IGenericRepository<Supplier> Supplier { get; }
        IGenericRepository<ProductCategory> ProductCategory { get; }
        IGenericRepository<Product> Product { get; }
        IGenericRepository<Bank> Bank { get; }
        IGenericRepository<Configuration> Configuration { get; }

        // Método genérico para obtener repositorios
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
        
    }


}