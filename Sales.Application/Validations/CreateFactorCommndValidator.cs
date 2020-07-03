using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands;
using Sales.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.Application.Validations
{
   public class CreateFactorCommndValidator : AbstractValidator<CreateFactorCommand>
    {
        public CreateFactorCommndValidator(ISalesDbContextReadOnly _salesDbContextReadOnly)
        {
            RuleFor(command => command.ProductIds)
                .NotEmpty().WithMessage("این فیلد اجباری است")
                .MustAsync(async (productIds, cancellation) =>
                {
                    var result = await _salesDbContextReadOnly.Product.AsNoTracking().AnyAsync(x => productIds.Contains(x.Id));

                    return result;
                }).WithMessage("محصول مورد نظر وجود ندارد!");

        }
    }
}
