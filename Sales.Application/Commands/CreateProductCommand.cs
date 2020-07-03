using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.Application.Commands
{

    public class CreateProductCommand : IRequest<bool>
    {
        [Display(Name = "نام کالا")]
        public string Name { get;  set; }

        public string UnitPricingType { get;  set; }
        [Display(Name = "قیمت هر واحد")]
        public decimal UnitPricingValue { get;  set; }
        [Display(Name = "واحد کالا")]
        public int ProductUnitId { get;  set; }

        public List<SelectListItem> ProductUnits { get; set; }
        //public CreateProductCommand(string name, string unitPricingType, decimal unitPricingValue, int productUnitId)
        //{
        //    Name = name;
        //    UnitPricingType = unitPricingType;
        //    UnitPricingValue = unitPricingValue;
        //    ProductUnitId = productUnitId;
        //}
    }
}
