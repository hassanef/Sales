using MediatR;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands;
using Sales.Application.IServices;
using Sales.Domain.AggerregatesModel.FactorAggregate;
using Sales.Domain.Context;
using Sales.Domain.IRepositories;
using Sales.Infrastructura.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Application.CommandHandlers
{
    public class CreateFactorCommandHandler : IRequestHandler<CreateFactorCommand, int>
    {

        readonly IFactorRepository _factorRepository;
        readonly ISalesDbContextReadOnly _salesDbContextReadOnly;
        public CreateFactorCommandHandler(IFactorRepository factorRepository,
                                          ISalesDbContextReadOnly salesDbContextReadOnly)
        {
            _factorRepository = factorRepository;
            _salesDbContextReadOnly = salesDbContextReadOnly;

        }

        public async Task<int> Handle(CreateFactorCommand request, CancellationToken cancellationToken)
        {
            decimal totalValue = 0;
            if (request == null)
                throw new ArgumentNullException("request");

            var products = await _salesDbContextReadOnly.Product
                                                        .AsNoTracking()
                                                        .Where(x => request.ProductIds.Contains(x.Id))
                                                        .ToListAsync();

            if (!products.Any())
                throw new Exception("Products not found!");

           foreach(var item in products)
           {
                totalValue = item.UnitPricing.Value + totalValue;
           }

            Factor factor = new Factor(totalValue);

            foreach (var item in products)
            {
                //for first selected product the quantity is 1
                factor.AddFactorDetail(item.Id, 1, item.UnitPricing.Value);
            }

            await _factorRepository.CreateAsyncUoW(factor);

            await _factorRepository.SaveChangesAsync();
           
            return factor.Id;
        }

    }
}