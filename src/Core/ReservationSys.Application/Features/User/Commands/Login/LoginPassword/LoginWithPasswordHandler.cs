using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;

namespace ReservationSys.Application.Features.User.Commands.Login.LoginPassword;

public class LoginWithPasswordHandler : IRequestHandler<LoginWithPasswordCommandRequest, BaseResponse<TokenResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtService _jwtService;

    public LoginWithPasswordHandler(UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager,
                                    IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<TokenResponse>> Handle(LoginWithPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user is null)
            return new("Phone number or password incorrect.", HttpStatusCode.BadRequest);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (!result.Succeeded)
            return new("Phone number or password incorrect.", HttpStatusCode.BadRequest);

        var token = await _jwtService.GenerateJwttoken(user);

        return new("Login successful.", token, true, HttpStatusCode.OK);
    }
}
