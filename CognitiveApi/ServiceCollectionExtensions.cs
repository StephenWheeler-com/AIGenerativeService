using Microsoft.Extensions.DependencyInjection;
using WTI.InsightDaily.UX.CognitiveApiNS.Interfaces;

namespace WTI.InsightDaily.UX.CognitiveApiNS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseCognitiveServices(this IServiceCollection services)
    {
        services.AddScoped<ICognitiveServices, CognitiveServices>();

        return services;
    }
}
