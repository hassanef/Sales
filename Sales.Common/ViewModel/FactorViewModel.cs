using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.ViewModel
{
    public class FactorViewModel
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int FactorDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUnitName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //public List<FactorDetailViewModel> FactorDetails { get; set; }
    }

    
}
