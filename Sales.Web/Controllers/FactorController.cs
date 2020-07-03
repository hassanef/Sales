using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.IServices;

namespace Sales.Web.Controllers
{
    public class FactorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFactorService _factorService;
        public FactorController(IMediator mediator, IFactorService factorService)
        {
            _mediator = mediator;
            _factorService = factorService;
        }
        [HttpGet]
        [Route("Factor/Index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var factor = await _factorService.GetFactorById(id);

            return View(factor);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterFactor([FromBody] CreateFactorCommand cmd)
        {
            int factorId = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    factorId = await _mediator.Send(cmd);
                }
                catch (Exception)
                {

                    throw new Exception("CreateFactorCommand has exception");
                }
                
            }

            return Ok(factorId);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody]UpdateQuantityFactorDetailCommand cmd)
        {
            int factorId = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    factorId = await _mediator.Send(cmd);
                }
                catch (Exception)
                {

                    throw new Exception("UpdateQuantityFactorDetailCommand has exception!");
                }
                 
            }

            return Ok(factorId);
        }
    }
}
