using MediatR;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Features.User.Commands.RefreshToken;

public class RefreshTokenCommandRequest: IRequest<BaseResponse<TokenResponse>>
{
    public string RefreshToken { get; set; }=null!;

    public string AccessToken { get; set; }=null!; 
}
