using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.DTOs.Email;
using ReservationSys.Application.Shared.Responses;
using ReservationSys.Domain.Entities;
using System.Net;
using System.Text;

namespace ReservationSys.Application.Features.User.Commands.Email.PasswordReset.SendResetEmail;

public class SendResetEmailCommandHandler : IRequestHandler<SendResetEmailCommandRequest, BaseResponse<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRabbitMQService _rabbitMQService;
    public SendResetEmailCommandHandler(UserManager<AppUser> userManager, IRabbitMQService rabbitMQService)
    {
        _userManager = userManager;
        _rabbitMQService = rabbitMQService;
    }
    public async Task<BaseResponse<string>> Handle(SendResetEmailCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return new("Email not found", false, HttpStatusCode.NotFound);

        var resetlink = await GetEmailResetConfirm(user);

        var emailMessage = new EmailMessageDto
        {
            To = new List<string> { user.Email },
            Subject = "Reset password",
            Body = resetlink
        };

        await _rabbitMQService.PublishAsync(emailMessage);
        return new("Reset link sent to your email", true, HttpStatusCode.OK);
    }
    private async Task<string> GetEmailResetConfirm(AppUser user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var link = $"https://localhost:7255/api/Users/ResetConfirmEmail?email={user.Email}&token={encodedToken}";
        Console.WriteLine("Reset Password Link : " + link);
        return link;
    }
}
