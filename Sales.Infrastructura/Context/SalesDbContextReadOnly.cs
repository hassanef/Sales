
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using Sales.Domain.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Infrastructura.Context
{
    public  class SalesDbContextReadOnly : DbContext, ISalesDbContextReadOnly
    {
        IConfiguration _configuration;
        private string _connectionString;


        public virtual DbSet<Product> Products { get; }
        public virtual DbSet<ProductUnit> ProductUnits { get; }
        public virtual DbSet<Factor> Factors { get; }
        public virtual DbSet<FactorDetail> FactorDetails { get; }

        public SalesDbContextReadOnly(IConfiguration configuration)
        {

            _configuration = configuration;
            _connectionString = configuration["ConnectionStrings:ConnectionString"];

            Products = Set<Product>();
            ProductUnits = Set<ProductUnit>();
            FactorDetails = Set<FactorDetail>();
            Factors = Set<Factor>();
        }

        public IQueryable<Product> Product
        {
            get { return this.Products; }
        }

        public IQueryable<ProductUnit> ProductUnit
        {
            get { return this.ProductUnits; }
        }
        public IQueryable<Factor> Factor
        {
            get { return this.Factors; }
        }
        public IQueryable<FactorDetail> FactorDetail
        {
            get { return this.FactorDetails; }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            object p = optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().OwnsOne(o => o.UnitPricing);
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new InvalidOperationException();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new InvalidOperationException();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new InvalidOperationException();
        }
    }
}