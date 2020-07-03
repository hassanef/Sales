using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands;
using Sales.Domain.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Validations
{
    public  class UpdateQuantityFactorDetailCommandValidator : AbstractValidator<UpdateQuantityFactorDetailCommand>
    {
        public UpdateQuantityFactorDetailCommandValidator(ISalesDbContextReadOnly _salesDbContextReadOnly)
        {
            RuleFor(command => command.FactorId)
                 .NotEmpty().WithMessage("این فیلد اجباری است")
                 .GreaterThan(0).WithMessage("مقدار باید بیشتر از صفر باشد")
                            .MustAsync(async (factorId, cancellation) =>
                            {
                                var result = await _salesDbContextReadOnly.Factor.AsNoTracking().AnyAsync(x => x.Id == factorId);

                                return result;
                            }).WithMessage("فاکتور وجود ندارد!");

            RuleFor(command => command.FactorDetailId)
               .NotEmpty().WithMessage("این فیلد اجباری است")
               .GreaterThan(0).WithMessage("مقدار باید بیشتر از صفر باشد")
                          .MustAsync(async (factorDetailId, cancellation) =>
                          {
                              var result = await _salesDbContextReadOnly.FactorDetail.AsNoTracking().AnyAsync(x => x.Id == factorDetailId);

                              return result;
                          }).WithMessage("جزییات فاکتور وجود ندارد!");

            RuleFor(command => command.ProductId)
               .NotEmpty().WithMessage("این فیلد اجباری است")
               .GreaterThan(0).WithMessage("مقدار باید بیشتر از صفر باشد")
                          .MustAsync(async (productId, cancellation) =>
                          {
                              var result = await _salesDbContextReadOnly.Product.AsNoTracking().AnyAsync(x => x.Id == productId);

                              return result;
                          }).WithMessage("کالا وجود ندارد!");

            RuleFor(command => command.Quantity)
               .NotEmpty().WithMessage("این فیلد اجباری است")
               .GreaterThan(0).WithMessage("مقدار باید بیشتر از صفر باشد");
        }
    }
}
