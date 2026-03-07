using KernUx.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KernUx.Blazor.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKernUx(this IServiceCollection services)
    {
        services.AddScoped<ThemeService>();
        services.AddScoped<IdGeneratorService>();

        return services;
    }
}

