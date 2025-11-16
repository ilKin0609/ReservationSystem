using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;

namespace ReservationSys.Application.Features.User.Commands.Login;

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
        var existEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existEmail is null)
            return new("Email or password is incorrect", HttpStatusCode.BadRequest);

        if (!existEmail.EmailConfirmed)
            return new("Please confirm your email address", HttpStatusCode.BadRequest);

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(existEmail, request.Password, true);

        if(!result.Succeeded)
            return new("Email or password is incorrect", HttpStatusCode.BadRequest);

        var token =await _jwtService.GenerateJwttoken(existEmail);

        return new("Login successfully",token, true, HttpStatusCode.OK);
    }
}
