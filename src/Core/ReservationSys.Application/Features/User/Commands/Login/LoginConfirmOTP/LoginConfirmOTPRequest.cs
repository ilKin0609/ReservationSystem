using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Login.LoginConfirmOTP;

public class LoginConfirmOTPRequest:IRequest<BaseResponse<TokenResponse>>
{
    public string PhoneNumber { get; set; } = null!;
    public string OTP { get; set; } = null!;
}
