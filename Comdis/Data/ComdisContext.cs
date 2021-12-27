using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Comdis.Models;
using Comdis.Comdis.Models;

    public class ComdisContext : DbContext
    {
        public ComdisContext (DbContextOptions<ComdisContext> options)
            : base(options)
        {
            
        }

        public DbSet<Comdis.Models.Product> Product { get; set; }

        public DbSet<Comdis.Models.UOM> UOM { get; set; }

        public DbSet<Comdis.Models.ProductCategory> ProductCategory { get; set; }

        public DbSet<Comdis.Models.Bank> Bank { get; set; }

        public DbSet<Comdis.Comdis.Models.Customer> Customer { get; set; }

        public DbSet<Comdis.Comdis.Models.Supplier> Supplier { get; set; }

        public DbSet<Comdis.Models.Sales> Sales { get; set; }

        public DbSet<Comdis.Models.SalesItems> SalesItems { get; set; }
    }
