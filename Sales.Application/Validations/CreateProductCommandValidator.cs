using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands;
using Sales.Domain.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Validations
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(ISalesDbContextReadOnly _salesDbContextReadOnly)
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("این فیلداجباری است")
                .MaximumLength(150).WithMessage("تعداد کاراکتر ها بیشتر از حد مجاز میباشد");

            RuleFor(command => command.ProductUnitId)
                .NotEmpty().WithMessage("این فیلد اجباری است")
                .GreaterThan(0).WithMessage("مقدار باید بیشتر از صفر باشد")
                           .MustAsync(async (productUnitId, cancellation) =>
                           {
                               var result = await _salesDbContextReadOnly.ProductUnit.AsNoTracking().AnyAsync(x => x.Id == productUnitId);
                         
                               return result;
                           }).WithMessage("واحد کالا وجود ندارد!");
            
            
            RuleFor(command => command.UnitPricingValue)
               .NotEmpty().WithMessage("این فیلد اجباری است")
                .GreaterThan(0).WithMessage("مقدار باید بیشتر از صفر باشد");
        }
    }
}
