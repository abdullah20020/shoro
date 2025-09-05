using MediatR;
using ApplicationLayer.DTOs.Auth;

namespace ApplicationLayer.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResponseDto>
    {
        public RegisterDto RegisterDto { get; }

        public RegisterCommand(RegisterDto registerDto)
        {
            RegisterDto = registerDto;
        }
    }
}
