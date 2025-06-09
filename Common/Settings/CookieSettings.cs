namespace Demo.GenerativeAI.Common.Settings;

public class CookieSettings
{
  public string Domain { get; set; } = null!;

    public int ExpireDays { get; set; }

    public string EncryptionKey { get; set; }

    public string Path { get; set; }
}
