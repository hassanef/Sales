using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.EntityConfiguration
{
    public class FactorDetailEntityTypeConfiguration : IEntityTypeConfiguration<FactorDetail>
    {

        public void Configure(EntityTypeBuilder<FactorDetail> factorDetailConfiguration)
        {
            factorDetailConfiguration.HasKey(o => o.Id);
         
            factorDetailConfiguration.Property(x => x.Quantity).IsRequired();
            factorDetailConfiguration.Property(x => x.TotalAmount).HasPrecision(10);

            factorDetailConfiguration.HasOne<Product>()
             .WithMany()
             .IsRequired(true)
             .HasForeignKey("ProductId");
        }
    }
}
