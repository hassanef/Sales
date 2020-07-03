using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.EntityConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.HasKey(o => o.Id);

            productConfiguration.Property(x => x.Name).HasMaxLength(150).IsRequired();
            productConfiguration.Property(x => x.ProductUnitId).IsRequired();
            

            productConfiguration.OwnsOne(o => o.UnitPricing).Property(x => x.Value).IsRequired();
            productConfiguration.OwnsOne(x => x.UnitPricing).Property(x => x.Value).HasPrecision(10);

            productConfiguration.HasOne<ProductUnit>()
              .WithMany()
              .IsRequired(true)
              .HasForeignKey("ProductUnitId");
        }
    }
}
