using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Common.ViewModel
{
    public class GridDataResult<T>
    {
        public List<T> data { get; set; }
        public int total { get; set; }
     
    }
}
