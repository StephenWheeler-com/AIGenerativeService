using Microsoft.Extensions.Primitives;

namespace WTI.InsightDaily.WebApi.HttpContextExtensions;

public interface IHttpRequestContext
{
    Guid GetTenantId();

    string GetSiteUrl();

    string GetBearerToken();

    List<string> GetClientIpAddress();

    IEnumerable<KeyValuePair<string, StringValues>> GetTrustedLocationHeaders();
}
