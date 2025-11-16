using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Login;

public class LoginWithPasswordCommandRequest:IRequest<BaseResponse<TokenResponse>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
