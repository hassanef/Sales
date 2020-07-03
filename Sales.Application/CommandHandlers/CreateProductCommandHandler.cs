using MediatR;
using Sales.Application.Commands;
using Sales.Domain.AggerregatesModel.ProductAggregate;
using Sales.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Application.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {

        readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var product = new Product(request.Name, request.UnitPricingType, request.UnitPricingValue, request.ProductUnitId);

            await _productRepository.CreateAsync(product);

            return true;
        }

    }
}
