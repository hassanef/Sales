using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.IServices;
using Sales.Domain.AggerregatesModel.ProductAggregate;

namespace Sales.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductQuery _productQuery;
        private readonly IProductUnitQuery _productUnitQuery;
        public ProductController(IMediator mediator,
                                 IProductQuery productQuery,
                                 IProductUnitQuery productUnitQuery)
        {
            _mediator = mediator;
            _productQuery = productQuery;
            _productUnitQuery = productUnitQuery;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Product()
        {
            var cmd = new CreateProductCommand();
            cmd.ProductUnits = await _productUnitQuery.GetProductUnits();

            return View(cmd);
        }
        public IActionResult ProductUnit()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductCommand cmd)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(cmd);
            }

            return RedirectToAction("Product");
        }
        [HttpPost]
        public async Task<IActionResult> AddProductUnit(CreateProductUnitCommand cmd)
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(cmd);
            }

            return RedirectToAction("ProductUnit");
        }
        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault() != null ? HttpContext.Request.Form["draw"].FirstOrDefault() : "";
            var start = Request.Form["start"].FirstOrDefault() != null ? int.Parse(Request.Form["start"].FirstOrDefault()) : 0;
            var length = Request.Form["length"].FirstOrDefault() != null ? int.Parse(Request.Form["length"].FirstOrDefault()) : 10;

            var products = await _productQuery.GetProducts(length, start);

            return Json(new
            {
                draw = draw,
                recordsFiltered = products.total,
                recordsTotal = products.total,
                data = products.data
            });
        }
        [HttpPost]
        public async Task<IActionResult> GetProductUnitData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault() != null ? HttpContext.Request.Form["draw"].FirstOrDefault() : "";
            var productUnits = await _productUnitQuery.GetProductUnits();


            return Json(new
            {
                draw = draw,
                recordsFiltered = 0,
                recordsTotal = 0,
                data = productUnits
            });
        }
    }
}
