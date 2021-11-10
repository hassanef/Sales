using Microsoft.EntityFrameworkCore;
using Sales.Application.IServices;
using Sales.Common.ViewModel;
using Sales.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Services
{
    public class  FactorQuery:IFactorQuery
    {
        private readonly ISalesDbContextReadOnly _salesDbContextReadOnly;

        public FactorQuery(ISalesDbContextReadOnly salesDbContextReadOnly)
        {
            _salesDbContextReadOnly = salesDbContextReadOnly;
        }
        public async Task<List<FactorViewModel>> GetFactorById(int factorId)
        {
            return await (from f in _salesDbContextReadOnly.Factor.AsNoTracking()
                          join fd in _salesDbContextReadOnly.FactorDetail.AsNoTracking() on f.Id equals fd.FactorId
                          join p in _salesDbContextReadOnly.Product.AsNoTracking() on fd.ProductId equals p.Id
                          join pu in _salesDbContextReadOnly.ProductUnit.AsNoTracking() on p.ProductUnitId equals pu.Id
                          where f.Id == factorId
                          select new FactorViewModel()
                          {
                              Id = f.Id,
                              TotalAmount = f.TotalAmount,
                              FactorDetailId = fd.Id,
                              ProductName = p.Name,
                              ProductUnitName = pu.Name,
                              Price = fd.TotalAmount,
                              ProductId = p.Id,
                              Quantity = fd.Quantity
                          }).ToListAsync();

        }
    }
}
