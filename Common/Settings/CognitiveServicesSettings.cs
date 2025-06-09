using IntellegenceService.Models;

namespace Demo.GenerativeAI.Common.Settings;

public class CognitiveServicesSettings
{
    public Dictionary<string, CompletionOptions> QueryOptions { get; set; } = null!;
}
