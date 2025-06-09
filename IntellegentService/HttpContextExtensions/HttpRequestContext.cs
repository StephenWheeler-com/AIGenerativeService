using Demo.GenerativeAI.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace WTI.InsightDaily.WebApi.HttpContextExtensions;

public class Claims
{
    public Guid TenantUId { get; set; }

    public string UPN {  get; set; }
}

public class HttpRequestContext : IHttpRequestContext
{
    private readonly IPAddress localHostAddress = IPAddress.Parse("127.0.0.1");

    private readonly IHttpContextAccessor httpContextAccessor;

    // STW -FIX 11-05-     private readonly IDataProtector dataProtector;

    private readonly AuthenticationSettings settings;

    // STW -FIX 11-05- public HttpRequestContext(IOptions<AuthenticationSettings> options, IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
    public HttpRequestContext(IOptions<AuthenticationSettings> options, IHttpContextAccessor httpContextAccessor)
    {
        settings = options.Value;

        this.httpContextAccessor = httpContextAccessor;

        // STW -FIX 11-05- dataProtector = dataProtectionProvider.CreateProtector(settings.CookieSettings.EncryptionKey);
    }

    public Guid GetTenantId()
    {
        Guid tenantUId = Guid.Empty;

        var jwt = GetBearerToken();
        if(string.IsNullOrWhiteSpace(jwt))
        {
            return tenantUId;
        }

        var handler = new JwtSecurityTokenHandler();

        var token = handler.ReadJwtToken(jwt);

        var claimType = @"tid";

        var tmpString = token.Claims.First(c => c.Type == claimType);

        if (Guid.TryParse(tmpString.Value, out tenantUId))
        {
        }

        return tenantUId;
    }

    public string GetSiteUrl()
    {
        var siteUrl = string.Empty;

        if(httpContextAccessor.HttpContext?.Request.Headers != null && !string.IsNullOrWhiteSpace(httpContextAccessor.HttpContext.Request.Headers.Origin))
        {
            siteUrl = httpContextAccessor.HttpContext.Request.Headers.Origin.ToString();
        }

        return siteUrl;
    }

    public string GetBearerToken()
    {
        var token = string.Empty;

        var authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        if(string.IsNullOrWhiteSpace(authorization))
        {
            return token;
        }

        if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = authorization.Substring("Bearer ".Length).Trim();
        }

        return token.Trim();
    }

    public IEnumerable<KeyValuePair<string, StringValues>> GetTrustedLocationHeaders()
    {
        var headers = (from c in httpContextAccessor.HttpContext?.Request.Headers
                       where c.Key.Contains("ITLC") || c.Key.Contains("ITLCFA")
                       select c);

        return headers;
    }

    public List<string> GetClientIpAddress()
    {
        var sourceIps = new HashSet<string>();

        var sourceIp = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        if (sourceIp != null)
        {
            sourceIps.Add(sourceIp);
        }

        sourceIp = httpContextAccessor.HttpContext?.GetServerVariable("REMOTE_HOST");
        if (sourceIp != null)
        {
            sourceIps.Add(sourceIp);
        }

        sourceIp = httpContextAccessor.HttpContext?.GetServerVariable("REMOTE_ADDR");
        if (sourceIp != null)
        {
            sourceIps.Add(sourceIp);
        }

        /*        
                var headerValues = (
                    from c in httpContextAccessor.HttpContext?.Request?.Headers
                    where c.Key.Equals("X-Forwarded-For") || c.Key.Equals("HTTP_CLIENT_IP") || c.Key.Equals("HTTP_X_CLUSTER_CLIENT_IP") || c.Key.Equals("REMOTE_ADDR")
                    select c.Value
                    );

                foreach(var item in headerValues)
                {
                    if(!IPAddress.TryParse(item, out IPAddress? ipAddress))
                    {
                        continue;
                    }

                    if (ipAddress.AddressFamily is not (AddressFamily.InterNetwork or AddressFamily.InterNetworkV6))
                    {
                        continue;
                    }

                    if(!Equals(ipAddress, localHostAddress))
                    {
                        sourceIps.Add(sourceIp);

                        return ipAddress.ToString();
                    }

                }

                var ipAddresses = new List<ClientIpAddress>();
                var dnsSourceIpAddresses = Dns.GetHostAddresses(Dns.GetHostName()).ToList();

                foreach(var ipAddress in dnsSourceIpAddresses.Where(ipAddresses => ipAddresses.AddressFamily is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6))
                {
                    if(localHostAddress != ipAddress)
                    {
                        ipAddresses.Add(new ClientIpAddress
                        {
                            Source = IpAddressSource.DnsLookup, Address = ipAddress, Family = ipAddress.AddressFamily
                        });
                    }
                }

                // Bias to IPv4
                var selectedIpAddress = ipAddresses.Find(c => Equals(c.Family, AddressFamily.InterNetwork));
                if(selectedIpAddress != null)
                {
                    return selectedIpAddress.Address.ToString();
                }

                // Last Chance, go with IPv6
                selectedIpAddress = ipAddresses.Find(c => Equals(c.Family, AddressFamily.InterNetworkV6));
                if(selectedIpAddress != null)
                {
                    return selectedIpAddress.Address.ToString();
                }
                */
        return sourceIps.ToList();
    }
}
