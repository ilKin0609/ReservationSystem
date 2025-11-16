using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Email.ConfirmEmail;

public class ConfirmEmailCommandRequest:IRequest<BaseResponse<string>>
{
    public string UserId { get; set; }=null!;
    public string Token { get; set; }=null!;
}
