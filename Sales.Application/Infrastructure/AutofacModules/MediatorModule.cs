using Autofac;
using Autofac.Core;
using FluentValidation;
using MediatR;
using Sales.Application.Behaviors;
using Sales.Application.Commands;
using Sales.Application.Validations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sales.Application.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            //// Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateFactorCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(CreateProductCommand).GetTypeInfo().Assembly)
                 .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(CreateProductUnitCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            
                 builder.RegisterAssemblyTypes(typeof(UpdateQuantityFactorDetailCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
              .RegisterAssemblyTypes(typeof(CreateProductCommandValidator).GetTypeInfo().Assembly)
              .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
              .AsImplementedInterfaces();

            builder
              .RegisterAssemblyTypes(typeof(CreateProductUnitCommandValidator).GetTypeInfo().Assembly)
              .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
              .AsImplementedInterfaces();



            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // builder.RegisterGeneric(typeof(TransactionalBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }

    }
}



