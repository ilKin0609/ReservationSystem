using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;
using System.Text;

namespace ReservationSys.Application.Features.User.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, BaseResponse<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOtpService _otpService;

    public UserRegisterCommandHandler(UserManager<AppUser> userManager, IOtpService otpService)
    {
        _userManager = userManager;
        _otpService = otpService;
    }

    public async Task<BaseResponse<string>> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (existingUser != null)
            return new("This phone number is already registered.", HttpStatusCode.BadRequest);

        await _otpService.SendOtpAsync(request);

        return new("OTP code sent successfully.", true, HttpStatusCode.OK);
    }
}

