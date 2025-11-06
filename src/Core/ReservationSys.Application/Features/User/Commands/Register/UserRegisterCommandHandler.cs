using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;
using System.Text;

namespace ReservationSys.Application.Features.User.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, BaseResponse<string>>
{
    public UserManager<AppUser> _userManager;
    public UserRegisterCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<BaseResponse<string>> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var existingUserEmail=await _userManager.FindByEmailAsync(request.Email);
        if (existingUserEmail is not null)
        {
            return new("Email is already registered.",HttpStatusCode.BadRequest);
        }

        var newUser = new AppUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email
        };

        var identityResult = await _userManager.CreateAsync(newUser, request.Password);
        if(!identityResult.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            return new(errorMessage.ToString(), HttpStatusCode.BadRequest);
        }

        return new("User registered successfully.",true, HttpStatusCode.Created);
    }
}
