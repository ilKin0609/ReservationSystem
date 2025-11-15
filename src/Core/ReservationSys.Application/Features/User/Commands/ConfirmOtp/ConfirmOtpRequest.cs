using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.ConfirmOtp;

public class ConfirmOtpRequest:IRequest<BaseResponse<string>>
{
    public string PhoneNumber { get; set; } = null!;
    public string OTP { get; set; }=null!;
}
