using Framework.DomainDrivenDesign.Domain.SeedWork;
using Sales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.AggerregatesModel.FactorAggregate
{
    public class TotalAmount : ValueObject
    {
        public string Type { get; private set; }
        public decimal Value { get; private set; }

        private TotalAmount() { }

        public TotalAmount(string type, decimal value)
        {
            if (value <= 0)
                throw new SalesException("TotalAmount.value is not valid!");

            //Type can be changing for any other countries money, but here just use static type
            Type = "Rial";
            Value = value;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Type;
            yield return Value;
        }
    }
}
