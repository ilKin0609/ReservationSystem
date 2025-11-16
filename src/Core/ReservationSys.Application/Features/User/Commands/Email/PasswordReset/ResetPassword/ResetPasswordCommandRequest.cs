using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.Email.PasswordReset.ResetPassword;

public class ResetPasswordCommandRequest : IRequest<BaseResponse<string>>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Password { get; set; } = null!;
}
