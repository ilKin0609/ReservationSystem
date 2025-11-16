using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;
using System.Security.Claims;

namespace ReservationSys.Application.Features.User.Commands.Email.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, BaseResponse<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ChangePasswordCommandHandler(UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<BaseResponse<string>> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var userId= _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return new("User not found", HttpStatusCode.NotFound);

        var user= await _userManager.FindByIdAsync(userId);
        if (user is null)
            return new("User not found", HttpStatusCode.NotFound);

        if(request.NewPassword != request.ConfirmNewPassword)
            return new("New password and confirmation do not match", HttpStatusCode.BadRequest);

        if(request.CurrentPassword == request.NewPassword)
            return new("New password must be different from the current password", HttpStatusCode.BadRequest);

        var checkPassword= await  _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        if (!checkPassword)
            return new("Current password is incorrect", HttpStatusCode.BadRequest);

        var result= await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new($"Password change failed: {errors}", HttpStatusCode.BadRequest);
        }
        return new("Password changed successfully", HttpStatusCode.OK);
    }
}
