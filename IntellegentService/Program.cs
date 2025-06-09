using Demo.GenerativeAI.Common.Settings;
using Demo.GenerativeAI.KeyVaultNS;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using WTI.InsightDaily.UX.CognitiveApiNS;
using WTI.InsightDaily.UX.DispatcherNS;
using WTI.MyDay.WebApi.Controllers;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>()
            .Build();

var builder = WebApplication.CreateBuilder(args);

builder?.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder?.Services.AddHttpContextAccessor();

builder?.Services.AddOptions<KeyVaultSettings>()
    .Configure<IConfiguration>((setting, configuration) =>
    {
        configuration.GetSection("KeyVaultSettings").Bind(setting);
    });

builder?.Services.AddOptions<CognitiveServicesSettings>()
    .Configure<IConfiguration>((setting, configuration) =>
    {
        configuration.GetSection("CognitiveServicesSettings").Bind(setting);
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder?.Services.AddEndpointsApiExplorer();

builder?.Services.UseKeyVault();
builder?.Services.UseWebApi();
builder?.Services.UseCognitiveServices();
builder?.Services.UseDispatcher();

// Add services to the container.

builder?.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder?.Services.AddOpenApi();

var app = builder?.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(o => o.SwaggerEndpoint(@"/openapi/v1.json", "Intellegent Service"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
