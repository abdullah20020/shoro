using ApplicationLayer.Features.Commands;
using ApplicationLayer.Features.Commend;
using ApplicationLayer.Features.Queries;
using DomianLayar.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace shora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 //   [Authorize]
    public class CasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CasesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
   
        public async Task<IActionResult> Create(Case newCase)
        {
            var result = await _mediator.Send(new CreateCommand<Case>(newCase));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Client,Lawyer,LawFirm")]
        public async Task<IActionResult> Update(Case updatedCase)
        {
            var result = await _mediator.Send(new UpdateCommand<Case>(updatedCase));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCommand<Case>(id));
            if (!result) return NotFound();
            return Ok("Deleted successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetByIdQuery<Case>(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuery<Case>());
            return Ok(result);
        }
    }
}