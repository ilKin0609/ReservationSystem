using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Security.Claims;

namespace ReservationSys.Application.Abstracts.Services;

public interface IJwtService
{
    Task<TokenResponse> GenerateJwttoken(AppUser user);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
