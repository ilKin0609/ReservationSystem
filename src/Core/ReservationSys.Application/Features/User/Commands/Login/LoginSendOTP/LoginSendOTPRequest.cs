using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Login.LoginSendOTP;

public class LoginSendOTPRequest:IRequest<BaseResponse<string>>
{
    public string PhoneNumber { get; set; } = null!;
}
