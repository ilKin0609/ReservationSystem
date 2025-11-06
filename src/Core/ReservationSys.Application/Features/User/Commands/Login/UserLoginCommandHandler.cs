using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;
using System.Text;

namespace ReservationSys.Application.Features.User.Commands.Login;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, BaseResponse<TokenResponse>>
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private IJwtService _jwtService;

    public UserLoginCommandHandler(UserManager<AppUser> userManager, 
                                SignInManager<AppUser> signInManager, 
                                IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<TokenResponse>> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var existingUser=await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is null)
            return new("Email or password incorrect",HttpStatusCode.NotFound);

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(existingUser, request.Password, true);
    
        if(!signInResult.Succeeded)
            return new("Email or password incorrect",HttpStatusCode.BadRequest);

        var token = await _jwtService.GenerateJwttoken(existingUser);

        return new("Login successfully",token,true,HttpStatusCode.OK);
    }
}
