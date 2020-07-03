using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.Domain.Context
{
    public interface ISalesDbContextReadOnly
    {
        IQueryable<Product> Product { get; }
        IQueryable<ProductUnit> ProductUnit { get; }
        IQueryable<Factor> Factor { get; }
        IQueryable<FactorDetail> FactorDetail{ get; }

    }
}
