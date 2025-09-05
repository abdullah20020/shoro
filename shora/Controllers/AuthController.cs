using ApplicationLayer.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationLayer.DTOs.Auth;

namespace shora.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto);
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var command = new RegisterCommand(registerDto);
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("You are authenticated!");
        }

        [HttpGet("admin")]
      //  [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok("You are an admin!");
        }

        [HttpGet("lawyer")]
        [Authorize(Roles = "Lawyer")]
        public IActionResult LawyerOnly()
        {
            return Ok("You are a lawyer!");
        }

        [HttpGet("lawfirm")]
        [Authorize(Roles = "LawFirm")]
        public IActionResult LawFirmOnly()
        {
            return Ok("You are a law firm!");
        }

        [HttpGet("client")]
        [Authorize(Roles = "Client")]
        public IActionResult ClientOnly()
        {
            return Ok("You are a client!");
        }
    }
}


