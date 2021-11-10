using Microsoft.AspNetCore.Mvc.Rendering;
using Sales.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.IServices
{
    public interface IProductQuery
    {
        Task<GridDataResult<ProductViewModel>> GetProducts(int pageSize, int pageNum);
    }
}
