using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ReservationSys.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSetting _emailSetting;

    public EmailService(IOptions<EmailSetting> emailSetting)
    {
        _emailSetting = emailSetting.Value;
    }

    public async Task SendEmailAsync(IEnumerable<string> toEmail, string subject, string body)
    {
        using var smtp = new SmtpClient(_emailSetting.SmtpServer, _emailSetting.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailSetting.SenderEmail, _emailSetting.Password),
            EnableSsl = true
        };

        using var message = new MailMessage
        {
            From = new MailAddress(_emailSetting.SenderEmail, _emailSetting.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        foreach (var email in toEmail.Distinct())
        {
            message.To.Add(email);
        }

        await smtp.SendMailAsync(message);

    }
}
