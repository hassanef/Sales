using Microsoft.EntityFrameworkCore;
using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using Sales.Infrastructura.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.Context
{
    // add-migration 'initial' -context Sales.Infrastructura.Context.SalesDbContext  
    // update-database 'initial' -context Sales.Infrastructura.Context.SalesDbContext 
    public class SalesDbContext:DbContext
    {
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorDetail> FactorDetails { get; set; }


        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {

            System.Diagnostics.Debug.WriteLine("SalesDbContext::ctor ->" + this.GetHashCode());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FactorDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductUnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FactorEntityTypeConfiguration());
        }
    }
}
