using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Login.LoginPassword;

public class LoginWithPasswordCommandRequest:IRequest<BaseResponse<TokenResponse>>
{
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}
