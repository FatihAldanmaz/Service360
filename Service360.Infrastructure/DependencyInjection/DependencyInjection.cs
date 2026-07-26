using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service360.Application.Interfaces;
using Service360.Infrastructure.Services;

namespace Service360.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}