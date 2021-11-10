using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Application.Commands
{

    public class CreateProductCommand : IRequest<bool>
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Required")]
        public string Name { get;  set; }
        public string UnitPricingType { get;  set; }
        [Display(Name = "Price per unit")]
        [Required(ErrorMessage = "Required")]
        public decimal UnitPricingValue { get;  set; }
        [Display(Name = "Product Unit")]
        [Required(ErrorMessage = "Required")]
        public int ProductUnitId { get;  set; }

        public List<SelectListItem> ProductUnits { get; set; }
    }
}
