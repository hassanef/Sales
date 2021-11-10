using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sales.Application.IServices;
using Sales.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Services
{
    public  class ProductUnitQuery : IProductUnitQuery
    {
        private readonly ISalesDbContextReadOnly _salesDbContextReadOnly;

        public ProductUnitQuery(ISalesDbContextReadOnly salesDbContextReadOnly)
        {
            _salesDbContextReadOnly = salesDbContextReadOnly;
        }


        public async Task<List<SelectListItem>> GetProductUnits()
        {
            return await _salesDbContextReadOnly.ProductUnit
                                                  .AsNoTracking()
                                                  .Select(x => new SelectListItem()
                                                  {
                                                      Value = x.Id.ToString(),
                                                      Text = x.Name
                                                  }).ToListAsync();

        }
    }
}