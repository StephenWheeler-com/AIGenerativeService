namespace Demo.GenerativeAI.KeyVaultNS.Interfaces;

public interface IKeyVault
{
    Task<string?> GetApiSecret();
}
