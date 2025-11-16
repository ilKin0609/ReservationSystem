using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReservationSys.Application.Abstracts.Services;
using ReservationSys.Application.DTOs.Email;
using ReservationSys.Application.Shared.Settings;
using System.Text;

namespace ReservationSys.Infrastructure.Services.EmailRabbitMQ;

public class EmailConsumer
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabbitMqSetting _settings;
    private readonly ILogger<EmailConsumer> _logger;

    public EmailConsumer(IServiceScopeFactory scopeFactory,
                         IOptions<RabbitMqSetting> options,
                         ILogger<EmailConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _settings = options.Value;
        _logger = logger;
    }

    public void Start()
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost,
            Ssl = new SslOption
            {
                Enabled = false,
                ServerName = _settings.Host,
                AcceptablePolicyErrors =
                    System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors |
                    System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch |
                    System.Net.Security.SslPolicyErrors.RemoteCertificateNotAvailable
            }
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(_settings.Exchange, ExchangeType.Direct, durable: true);
        channel.QueueDeclare(_settings.Queue, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(_settings.Queue, _settings.Exchange, _settings.RoutingKey);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var dto = JsonConvert.DeserializeObject<EmailMessageDto>(json);

            using var scope = _scopeFactory.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            try
            {
                await emailService.SendEmailAsync(dto.To, dto.Subject, dto.Body);

                _logger.LogInformation("✅ Email uğurla göndərildi. Ünvan: {Email}", dto.To);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Email göndərilə bilmədi. Ünvan: {Email}", dto.To);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            }
        };

        channel.BasicConsume(
            queue: _settings.Queue,
            autoAck: false,
            consumer: consumer
        );
    }
}
