using MediatR;
using ApplicationLayer.DTOs.Auth;

namespace ApplicationLayer.Features.Auth
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public LoginDto LoginDto { get; set; }

        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
