using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Common.ViewModel
{
    public class FactorDetailViewModel
    {
        public int Id { get; set; }
        public int FactorId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUnitName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
