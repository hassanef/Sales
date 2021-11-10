using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sales.Application.IServices;
using Sales.Common.ViewModel;
using Sales.Domain.Context;
using Sales.Infrastructura.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Services
{
    public class ProductQuery:IProductQuery
    {
        private readonly ISalesDbContextReadOnly _salesDbContextReadOnly;

        public ProductQuery(ISalesDbContextReadOnly salesDbContextReadOnly)
        {
            _salesDbContextReadOnly = salesDbContextReadOnly;
        }


        public async Task<GridDataResult<ProductViewModel>> GetProducts(int pageSize, int pageNum)
        {
            var gridData = new GridDataResult<ProductViewModel>();
            gridData.total = await _salesDbContextReadOnly.Product.AsNoTracking().CountAsync();

            gridData.data = await (from p in _salesDbContextReadOnly.Product
                                                           .AsNoTracking()
                                                           .Skip(pageNum)
                                                           .Take(pageSize)
                                join pu in _salesDbContextReadOnly.ProductUnit.AsNoTracking()
                                on p.ProductUnitId equals pu.Id
                                select new ProductViewModel()
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    Price = p.UnitPricing.Value,
                                    UnitName = pu.Name
                                }).ToListAsync();

            return gridData;
        }
        

    }
}
