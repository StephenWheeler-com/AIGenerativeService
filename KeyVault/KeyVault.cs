using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Demo.GenerativeAI.Common.Settings;
using Demo.GenerativeAI.KeyVaultNS.Interfaces;

namespace Demo.GenerativeAI.KeyVaultNS;

public class KeyVault : IKeyVault
{
    private readonly ILogger<KeyVault> logger;

    private readonly KeyVaultSettings settings = null!;

    public KeyVault(IOptions<KeyVaultSettings> options, ILogger<KeyVault> logger)
    {
        settings = options.Value;

        this.logger = logger;
    }

    public async Task<string?> GetApiSecret()
    {
#if DEBUG
        // This is used when local debugging
        return settings.SecretValue ;
#endif
        var vaultUri = new Uri($"https://{settings.KeyVaultName}.vault.azure.net/");

        var client = new SecretClient(vaultUri: vaultUri, credential: new DefaultAzureCredential());

        var secretName = await client.GetSecretAsync(settings.SecretName);

        return secretName.Value.Value;
    }
}
