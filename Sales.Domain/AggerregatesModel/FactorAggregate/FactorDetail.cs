using Framework.DomainDrivenDesign.Domain.SeedWork;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.AggerregatesModel.FactorAggregate
{
   public  class FactorDetail : Entity
    {
        public int FactorId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CreateDate { get; private set; }


        public FactorDetail(int productId,  int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
            CreateDate = DateTime.Now;
        }

        public void SetTotalAmount(int quantity, decimal price)
        {
            //here should put clause about business
            if (quantity <= 0 || price <= 0)
                throw new Exception("Quantity or Price isn't valid!");

            TotalAmount = quantity * price;
        }
        public void SetQuantity(int quantity) => Quantity = quantity;
  
    }
}
