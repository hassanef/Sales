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
        private readonly IProductService _productService;
        private readonly IProductUnitService _productUnitService;
        public ProductController(IMediator mediator,
                                 IProductService productService, 
                                 IProductUnitService productUnitService)
        {
            _mediator = mediator;
            _productService = productService;
            _productUnitService = productUnitService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Product()
        {
            var cmd = new CreateProductCommand();
            cmd.ProductUnits = await _productUnitService.GetProductUnits();

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
                try
                {
                    var result = await _mediator.Send(cmd);
                }
                catch (Exception)
                {

                    throw new Exception("CreateProductCommand has error!");
                }
            }

            return RedirectToAction("Product");
        }
        [HttpPost]
        public async Task<IActionResult> AddProductUnit(CreateProductUnitCommand cmd)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _mediator.Send(cmd);
                }
                catch (Exception)
                {

                    throw new Exception("CreateProductUnitCommand has error!");
                }
            }

            return RedirectToAction("ProductUnit");
        }
        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault() != null ? HttpContext.Request.Form["draw"].FirstOrDefault() : "";
            var start = Request.Form["start"].FirstOrDefault() != null ? int.Parse(Request.Form["start"].FirstOrDefault()) : 0;
            var length = Request.Form["length"].FirstOrDefault() != null ? int.Parse(Request.Form["length"].FirstOrDefault()) : 10;

            var productList = await _productService.GetProducts(length, start);

            return Json(new
            {
                draw = draw,
                recordsFiltered = productList.total,
                recordsTotal = productList.total,
                data = productList.data
            });
        }
        [HttpPost]
        public async Task<IActionResult> GetProductUnitData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault() != null ? HttpContext.Request.Form["draw"].FirstOrDefault() : "";
            var lst = await _productUnitService.GetProductUnits();

           
            return Json(new
            {
                draw = draw,
                recordsFiltered = 1000,
                recordsTotal = 1000,
                data = lst
            });
        }
    }
}
