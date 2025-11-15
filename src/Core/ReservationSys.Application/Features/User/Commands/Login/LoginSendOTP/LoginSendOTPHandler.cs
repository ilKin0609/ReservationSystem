using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Features.User.Commands.Register;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;

namespace ReservationSys.Application.Features.User.Commands.Login.LoginSendOTP;

public class LoginSendOTPHandler : IRequestHandler<LoginSendOTPRequest, BaseResponse<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOtpService _otpService;

    public LoginSendOTPHandler(UserManager<AppUser> userManager, IOtpService otpService)
    {
        _userManager = userManager;
        _otpService = otpService;
    }

    public async Task<BaseResponse<string>> Handle(LoginSendOTPRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user is null)
            return new("Phone number not found.", HttpStatusCode.NotFound);

        await _otpService.SendOtpAsync(new UserRegisterCommandRequest
        {
            PhoneNumber = request.PhoneNumber,
            FullName = user.FullName,
            Email = user.Email,
            Password = "" 
        });

        return new("OTP sent successfully.", true, HttpStatusCode.OK);
    }
}
