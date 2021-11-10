using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.IServices
{
    public interface IProductUnitQuery
    {
        Task<List<SelectListItem>> GetProductUnits();
    }
}
