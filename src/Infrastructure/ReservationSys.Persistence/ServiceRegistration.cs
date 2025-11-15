using Microsoft.Extensions.DependencyInjection;
using ReservationSys.Infrastructure.Services;

using ReservationSys.Application.Abstracts.Services;

namespace ReservationSys.Persistence;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        #region Repositories

        #endregion

        #region Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddHttpClient<IOtpService, InfobipOtpService>();
        #endregion
    }
}
