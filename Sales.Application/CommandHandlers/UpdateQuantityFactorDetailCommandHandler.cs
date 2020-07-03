using MediatR;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands;
using Sales.Domain.Context;
using Sales.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Application.CommandHandlers
{
    public  class UpdateQuantityFactorDetailCommandHandler : IRequestHandler<UpdateQuantityFactorDetailCommand, int>
    {

        readonly IFactorRepository _factorRepository;
        readonly ISalesDbContextReadOnly _salesDbContextReadOnly;
        public UpdateQuantityFactorDetailCommandHandler(IFactorRepository factorRepository,
                                                        ISalesDbContextReadOnly salesDbContextReadOnly)
        {
            _factorRepository = factorRepository;
            _salesDbContextReadOnly = salesDbContextReadOnly;

        }

        public async Task<int> Handle(UpdateQuantityFactorDetailCommand request, CancellationToken cancellationToken)
        {
            decimal totalValue = 0;
            if (request == null)
                throw new ArgumentNullException("request");

            var product = await _salesDbContextReadOnly.Product
                                                        .AsNoTracking()
                                                        .SingleOrDefaultAsync(x => x.Id == request.ProductId);

            if (product == null)
                throw new ArgumentNullException("product");

            var factor = await _factorRepository.FetchAll()
                                                  .Include(x => x.FactorDetails)
                                                  .SingleOrDefaultAsync(x => x.Id == request.FactorId);

            if (factor == null)
                throw new ArgumentNullException("factor");

            factor.FactorDetails.Single(x => x.Id == request.FactorDetailId)
                                .SetTotalAmount(request.Quantity, product.UnitPricing.Value);

            factor.FactorDetails.Single(x => x.Id == request.FactorDetailId)
                             .SetQuantity(request.Quantity);

            foreach (var item in factor.FactorDetails)
            {
                totalValue = item.TotalAmount + totalValue;
            }

            factor.SetTotalAmount(totalValue);

             _factorRepository.UpdateUoW(factor);
            
            await _factorRepository.SaveChangesAsync();

            return factor.Id;
        }

    }
}