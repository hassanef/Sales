using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Common.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public string UnitName { get;  set; }
        public int Count { get; set; }
    }
}
