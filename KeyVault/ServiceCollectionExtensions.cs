using Microsoft.Extensions.DependencyInjection;
using Demo.GenerativeAI.KeyVaultNS.Interfaces;

namespace Demo.GenerativeAI.KeyVaultNS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseKeyVault(this IServiceCollection services)
    {
        services.AddScoped<IKeyVault, KeyVault>();

        return services;
    }
}
