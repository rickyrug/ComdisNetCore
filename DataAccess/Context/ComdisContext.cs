using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;


    public class ComdisContext : DbContext
    {
        public ComdisContext (DbContextOptions<ComdisContext> options)
            : base(options)
        {
            
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ComdisDB.db", b => b.MigrationsAssembly("Comdis"));
    }

    public DbSet<Product> Product { get; set; }

        public DbSet<UOM> UOM { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<Bank> Bank { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesItems> SalesItems { get; set; }

        public DbSet<Configuration> Configuration { get; set; }

        public DbSet<Purchase> Purchase { get; set; }

        public DbSet<PurchaseItems> PurchaseItems { get; set; }

        public DbSet<PriceList> PriceList { get; set; }
}
