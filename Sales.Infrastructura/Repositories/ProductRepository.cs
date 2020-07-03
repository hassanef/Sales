using Microsoft.AspNetCore.Http;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using Sales.Domain.IRepositories;
using Sales.Infrastructura.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.Repositories
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        readonly SalesDbContext _dbContext;
        public ProductRepository(SalesDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            _dbContext = dbContext;
        }

    }
}
