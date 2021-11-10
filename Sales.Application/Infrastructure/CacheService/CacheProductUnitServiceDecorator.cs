using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Sales.Application.IServices;
using Sales.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Infrastructure.CacheService
{
    public class CacheProductUnitServiceDecorator:IProductUnitQuery
    {
        private readonly IProductUnitQuery productService;
        private readonly IMemoryCache cache;
        private const string MyModelCacheKey = "myModelCacheKey";
        private MemoryCacheEntryOptions cacheOptions;

        // alternatively use IDistributedCache if you use redis and multiple services
        public CacheProductUnitServiceDecorator(IProductUnitQuery productService, IMemoryCache cache)
        {
            this.productService = productService;
            this.cache = cache;

            // 1 day caching
            cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(relative: TimeSpan.FromDays(1));
        }

        public async Task<List<SelectListItem>> GetProductUnits()
        {
            // Check cache
            var value = cache.Get<List<SelectListItem>>("myModelCacheKey");
            if (value == null)
            {
                // Not found, get from DB
                value = await productService.GetProductUnits();

                // write it to the cache
                cache.Set("myModelCacheKey", value, cacheOptions);
            }

            return value;
        }

    }
}