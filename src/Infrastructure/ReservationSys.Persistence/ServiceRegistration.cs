using Microsoft.Extensions.DependencyInjection;
using ReservationSys.Infrastructure.Services;

using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Infrastructure.Services.EmailRabbitMQ;

namespace ReservationSys.Persistence;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        #region Repositories

        #endregion

        #region Services
        services.AddSingleton<EmailConsumer>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IRabbitMQService, RabbitMqEmailQueueService>();
        services.AddHostedService<EmailConsumerService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddHttpClient<IOtpService, InfobipOtpService>();
        #endregion
    }
}
