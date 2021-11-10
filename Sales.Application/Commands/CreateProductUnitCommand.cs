using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sales.Application.Commands
{
    public class CreateProductUnitCommand : IRequest<bool>
    {
        [Display(Name = "Unit name")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

    }
}
