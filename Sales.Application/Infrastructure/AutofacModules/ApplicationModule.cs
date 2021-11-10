using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Sales.Application.Infrastructure.CacheService;
using Sales.Application.IServices;
using Sales.Application.Services;
using Sales.Domain.Context;
using Sales.Domain.IRepositories;
using Sales.Infrastructura.Context;
using Sales.Infrastructura.Repositories;

namespace Sales.Application.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FactorRepository>().As<IFactorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductUnitRepository>().As<IProductUnitRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductQuery>().As<IProductQuery>().InstancePerLifetimeScope();
            builder.RegisterType<FactorQuery>().As<IFactorQuery>().InstancePerLifetimeScope();
            builder.RegisterType<SalesDbContextReadOnly>().As<ISalesDbContextReadOnly>().InstancePerLifetimeScope();
            builder.RegisterType<ProductUnitQuery>().As<IProductUnitQuery>().InstancePerLifetimeScope();

           
        }
    }
}
