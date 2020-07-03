using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.AggerregatesModel.FactorAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.EntityConfiguration
{
    public class FactorEntityTypeConfiguration : IEntityTypeConfiguration<Factor>
    {

        public void Configure(EntityTypeBuilder<Factor> factorConfiguration)
        {
            factorConfiguration.HasKey(o => o.Id);

            factorConfiguration.Property(x => x.TotalAmount).IsRequired();
            factorConfiguration.Property(x => x.TotalAmount).HasPrecision(10);
        }
    }
}
