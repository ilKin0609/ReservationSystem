using ReservationSys.Application.DTOs.Email;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Abstracts.Services;

public interface IEmailService
{
    Task SendEmailAsync(IEnumerable<string> toEmail, string subject, string body);
}
