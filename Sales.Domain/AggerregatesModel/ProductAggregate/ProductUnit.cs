using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.AggerregatesModel.ProductAggregate
{
    public class ProductUnit:Entity
    {
        public string Name { get; private set; }
        public DateTime CreateDate { get; private set; }

        public ProductUnit(string name)
        {
            Name = name;
            CreateDate = DateTime.Now;
        }

    }
}
