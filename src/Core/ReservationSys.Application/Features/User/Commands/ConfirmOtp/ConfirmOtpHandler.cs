using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Features.User.Commands.Register;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;

namespace ReservationSys.Application.Features.User.Commands.ConfirmOtp;

public class ConfirmOtpHandler : IRequestHandler<ConfirmOtpRequest, BaseResponse<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOtpService _otpService;

    public ConfirmOtpHandler(UserManager<AppUser> userManager,IOtpService otpService)
    {
        _userManager = userManager;
        _otpService = otpService;
    }

    public async Task<BaseResponse<string>> Handle(ConfirmOtpRequest request, CancellationToken cancellationToken)
    {
        var pendingUser = _otpService.ValidateOtp(request.PhoneNumber, request.OTP);
        if (pendingUser == null)
            return new("Invalid or expired OTP.", HttpStatusCode.BadRequest);

        var user = new AppUser
        {
            FullName = pendingUser.FullName,
            Email = pendingUser.Email,
            UserName = pendingUser.PhoneNumber,
            PhoneNumber = pendingUser.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, pendingUser.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return new(errors, HttpStatusCode.BadRequest);
        }

        return new("User registered successfully.", true, HttpStatusCode.OK);
    }
}
