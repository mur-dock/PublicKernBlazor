using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Extensions;

/// <summary>
/// Erweiterungsmethoden zur Registrierung der PublicKernBlazor.Components-Dienste im DI-Container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registriert alle für PublicKernBlazor.Components benötigten Dienste (<see cref="ThemeService"/> und <see cref="IdGeneratorService"/>).
    /// </summary>
    /// <param name="services">Der <see cref="IServiceCollection"/>, dem die Dienste hinzugefügt werden.</param>
    /// <returns>Dieselbe <see cref="IServiceCollection"/> zur Method-Chaining-Unterstützung.</returns>
    public static IServiceCollection AddKernUx(this IServiceCollection services)
    {
        services.AddScoped<ThemeService>();
        services.AddScoped<IdGeneratorService>();

        return services;
    }
}
