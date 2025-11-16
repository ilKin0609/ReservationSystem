using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Email.PasswordReset.SendResetEmail;

public class SendResetEmailCommandRequest:IRequest<BaseResponse<string>>
{
    public string Email { get; set; }=null!;
}
