using MediatR;
using ApplicationLayer.DTOs.Auth;

namespace ApplicationLayer.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public LoginDto LoginDto { get; }

        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
