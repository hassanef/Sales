using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.IServices;
using System.Threading.Tasks;

namespace Sales.Web.Controllers
{
    public class FactorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFactorQuery _factorQuery;
        public FactorController(IMediator mediator, IFactorQuery factorQuery)
        {
            _mediator = mediator;
            _factorQuery = factorQuery;
        }
        [HttpGet]
        [Route("Factor/Index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var factor = await _factorQuery.GetFactorById(id);

            return View(factor);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterFactor([FromBody] CreateFactorCommand cmd)
        {
            int factorId = 0;

            if (ModelState.IsValid)
            {
                factorId = await _mediator.Send(cmd);
            }

            return Ok(factorId);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityFactorDetailCommand cmd)
        {
            int factorId = 0;

            if (ModelState.IsValid)
            {
                factorId = await _mediator.Send(cmd);
            }

            return Ok(factorId);
        }
    }
}
