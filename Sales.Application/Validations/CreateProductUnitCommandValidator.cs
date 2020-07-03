using FluentValidation;
using Sales.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Validations
{
    public class CreateProductUnitCommandValidator : AbstractValidator<CreateProductUnitCommand>
    {
        public CreateProductUnitCommandValidator()
        {
            RuleFor(command => command.Name)
                      .NotEmpty().WithMessage("این فیلد اجباری است")
                        .MaximumLength(50).WithMessage("تعداد کاراکتر ها بیشتر از حد مجاز میباشد");
        }
    }
}
