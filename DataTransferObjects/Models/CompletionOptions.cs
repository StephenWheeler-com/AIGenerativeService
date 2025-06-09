namespace IntellegenceService.Models;

public class CompletionOptions
{
    public string? EndPointUrl { get; set; } = null;

    public string? ModelName { get; set; } = null;

    public int MaxOutputTokenCount { get; set; } = 400;

    public float Temperature { get; set; } = 1.0f;

    public float TopP { get; set; } = 1.0f;

    public float FrequencyPenalty { get; set; } = 0.0f;

    public float PresencePenalty { get; set; } = 0.0f;

    public StructuredOutput? StructedOutput { get; set; } = null;
}
