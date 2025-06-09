using WTI.InsightDaily.UX.DispatcherNS.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WTI.InsightDaily.UX.DispatcherNS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}
