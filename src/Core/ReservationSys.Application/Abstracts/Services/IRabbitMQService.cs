using ReservationSys.Application.DTOs.Email;
using ReservationSys.Application.Shared.Responses;

namespace ReservationSys.Application.Abstracts.Services;

public interface IRabbitMQService
{
    Task<EmailQueueResponse> PublishAsync(EmailMessageDto email);
}
