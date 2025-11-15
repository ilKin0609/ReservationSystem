using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;

namespace ReservationSys.Application.Features.User.Commands.Login.LoginConfirmOTP;

public class LoginConfirmOTPHandler:IRequestHandler<LoginConfirmOTPRequest,BaseResponse<TokenResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOtpService _otpService;
    private readonly IJwtService _jwtService;

    public LoginConfirmOTPHandler(UserManager<AppUser> userManager,
                                  IOtpService otpService,
                                  IJwtService jwtService)
    {
        _userManager = userManager;
        _otpService = otpService;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<TokenResponse>> Handle(LoginConfirmOTPRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user is null)
            return new("Phone number not found.", HttpStatusCode.NotFound);

        var valid = _otpService.ValidateOtp(request.PhoneNumber, request.OTP);
        if (valid is null)
            return new("Invalid or expired OTP.", HttpStatusCode.BadRequest);

        var token = await _jwtService.GenerateJwttoken(user);

        return new("Login successful.", token, true, HttpStatusCode.OK);
    }
}
