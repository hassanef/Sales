using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Sales.Application.Commands;
using Sales.Application.IServices;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using Sales.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Application.CommandHandlers
{
    class CreateProductUnitCommandHandler : IRequestHandler<CreateProductUnitCommand, bool>
    {
        private readonly IProductUnitQuery _productService;
        readonly IProductUnitRepository _productUnitRepository;
        private readonly IMemoryCache cache;
        private const string MyModelCacheKey = "myModelCacheKey";
        private MemoryCacheEntryOptions cacheOptions;
        public CreateProductUnitCommandHandler(IProductUnitRepository productUnitRepository, 
                                               IProductUnitQuery productUnitService, 
                                               IMemoryCache cache)
        {
            _productUnitRepository = productUnitRepository;
            _productService = productUnitService;
            this.cache = cache;

            // 1 day caching
            cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(relative: TimeSpan.FromDays(1));
        }

        public async Task<bool> Handle(CreateProductUnitCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var productUnit = new ProductUnit(request.Name);

            await _productUnitRepository.CreateAsync(productUnit);

            await UpdateProductUnitCache();

            return true;
        }
        private async Task UpdateProductUnitCache()
        {
            var value = cache.Get<List<SelectListItem>>("myModelCacheKey");
            // Not found, get from DB
            value = await _productUnitRepository.FetchAll()
                                                .Select(x => new SelectListItem()
                                                  {
                                                      Value = x.Id.ToString(),
                                                      Text = x.Name
                                                  }).ToListAsync();

            // write it to the cache
            cache.Set("myModelCacheKey", value, cacheOptions);
            
        }

    }
}
