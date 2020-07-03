using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.EntityConfiguration
{
    public class ProductUnitEntityTypeConfiguration : IEntityTypeConfiguration<ProductUnit>
    {

        public void Configure(EntityTypeBuilder<ProductUnit> productUnitConfiguration)
        {
            productUnitConfiguration.HasKey(o => o.Id);

            productUnitConfiguration.Property(x => x.Name).HasMaxLength(50).IsRequired();
            

        }
    }
}
