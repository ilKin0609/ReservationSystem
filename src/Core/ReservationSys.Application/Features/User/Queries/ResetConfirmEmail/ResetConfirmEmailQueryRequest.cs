using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Queries.ResetConfirmEmail;

public class ResetConfirmEmailQueryRequest : IRequest<ResetPasswordModelResponse>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
