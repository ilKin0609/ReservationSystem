using MediatR;
using Microsoft.AspNetCore.Identity;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.DTOs.Email;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;
using System.Text;
using System.Web;

namespace ReservationSys.Application.Features.User.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, BaseResponse<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRabbitMQService _mailService;

    public UserRegisterCommandHandler(UserManager<AppUser> userManager, IRabbitMQService mailService)
    {
        _userManager = userManager;
        _mailService = mailService;
    }

    public async Task<BaseResponse<string>> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
    {
     
        var existEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existEmail is not null)
            return new("This email is already register.",HttpStatusCode.BadRequest);

        var user = new AppUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
        };

        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            StringBuilder errors = new();
            foreach (var error in identityResult.Errors)
            {
                errors.AppendLine(error.Description);
            }
            return new(errors.ToString(), HttpStatusCode.BadRequest);
        }

        var emailConfirmLink = await GetEmailConfirm(user);

        var emailMessage = new EmailMessageDto
        {
            To = new List<string> { user.Email },
            Subject = "Welcome to ReservationSys",
            Body = emailConfirmLink
        };

        await _mailService.PublishAsync(emailMessage);

        return new("User registered successfully.",true, HttpStatusCode.OK);
    }

    private async Task<string> GetEmailConfirm(AppUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var emailConfirmLink = $"https://localhost:7255/api/Users/ConfirmEmail?token={HttpUtility.UrlEncode(token)}&userId={user.Id}";
        return emailConfirmLink;
    }
}

