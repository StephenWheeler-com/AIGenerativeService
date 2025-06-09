namespace Demo.GenerativeAI.Common.Settings;

public class KeyVaultSettings
{
    public string? KeyVaultName { get; set; } = null;

    public string? SecretName { get; set; } = null!;

    public string? SecretValue { get; set; } = null!;
}
