
using WTI.InsightDaily.WebApi.HttpContextExtensions;

namespace WTI.MyDay.WebApi.Controllers;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection UseWebApi(this IServiceCollection services)
    {
        services.AddScoped<IHttpRequestContext, HttpRequestContext>();

        return services;

    }
}