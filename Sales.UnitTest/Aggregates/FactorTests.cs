using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.Exceptions;
using System;
using Xunit;

namespace Sales.UnitTest.Aggregates
{
    public class FactorTests
    {
        [Fact]
        public void CreateFactor_WithZeroTotalAmount_Then_ReturnException()
        {
            Assert.Throws<SalesException>(() => new Factor(0));
        }
        [Fact]
        public void CreateFactor_WithInvalidQuantity_Then_ReturnException()
        {
            var factor = new Factor(1000);

            Assert.Throws<SalesException>(() => factor.AddFactorDetail(1, 0, 100));
        }
        [Fact]
        public void CreateFactor_WithInvalidPrice_Then_ReturnException()
        {
            var factor = new Factor(1000);

            Assert.Throws<SalesException>(() => factor.AddFactorDetail(1, 1, 0));
        }
        [Fact]
        public void CreateFactor_WithValidData_Then_ReturnNotNull()
        {
            var factor = new Factor(1000);
            factor.AddFactorDetail(1, 1, 10);

            Assert.NotNull(factor);
        }
    }
}
