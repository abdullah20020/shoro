using MediatR;
using ApplicationLayer.DTOs.Auth;

namespace ApplicationLayer.Features.Auth
{
    public class RegisterCommand : IRequest<AuthResponseDto>
    {
        public RegisterDto RegisterDto { get; set; }

        public RegisterCommand(RegisterDto registerDto)
        {
            RegisterDto = registerDto;
        }
    }
}
