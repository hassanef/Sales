using Framework.DomainDrivenDesign.Domain.SeedWork;
using Sales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.Domain.AggerregatesModel.FactorAggregate
{
    public class Factor : Entity, IAggregateRoot
    {
        private readonly List<FactorDetail> _factorDetails;

        public decimal TotalAmount { get;private set; }
        public DateTime CreateDate { get;private set; }

        public IReadOnlyCollection<FactorDetail> FactorDetails => _factorDetails;
        //public int CustomerId { get; set; }
        public Factor(decimal totalAmount)
        {
            if (totalAmount <= 0)
                throw new SalesException("totalAmount is not valid!");

            TotalAmount = totalAmount;
            CreateDate = DateTime.Now;
            _factorDetails = new List<FactorDetail>();
        }
        protected Factor()
        {
            _factorDetails = new List<FactorDetail>();
        }
        public void AddFactorDetail(int productId, int quantity, decimal price)
        {
            var existingFactorDetailForProduct = _factorDetails.Where(o => o.ProductId == productId)
             .SingleOrDefault();

            if(existingFactorDetailForProduct == null)
            {
                var factorDetail = new FactorDetail(productId, quantity);
                factorDetail.SetTotalAmount(quantity, price);
                _factorDetails.Add(factorDetail);
            }
        }
        public void SetTotalAmount(decimal totalAmount) => TotalAmount = totalAmount;

    }
}

