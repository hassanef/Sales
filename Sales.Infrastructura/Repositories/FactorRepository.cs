using Microsoft.AspNetCore.Http;
using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.IRepositories;
using Sales.Infrastructura.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructura.Repositories
{
    public class FactorRepository:Repository<Factor>, IFactorRepository
    {
        readonly SalesDbContext _dbContext;
        public FactorRepository(SalesDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            _dbContext = dbContext;
        }
    }
}
