using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Email.ChangePassword;

public class ChangePasswordCommandRequest:IRequest<BaseResponse<string>>
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}
