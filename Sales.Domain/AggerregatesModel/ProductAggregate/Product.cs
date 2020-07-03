using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Sales.Domain.AggerregatesModel.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public DateTime CreateDate { get; private set; }
        public UnitPricing UnitPricing { get; private set; }
        public int ProductUnitId { get; private set; }

        public Product(string name, string unitPricingType, decimal unitPricingValue, int productUnitId)
        {
            Name = name;
            CreateDate = DateTime.Now;
            ProductUnitId = productUnitId;
            UnitPricing = new UnitPricing(unitPricingType, unitPricingValue);
        }
        public Product()
        { }
    }
}
