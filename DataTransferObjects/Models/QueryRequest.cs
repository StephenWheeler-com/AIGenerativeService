namespace IntellegenceService.Models;

public class QueryRequest
{
    public string QueryName { get; set; } = string.Empty;

    public string[]? SystemPrompts { get; set; } = null;

    public string[]? UserPrompts { get; set; } = null;

    // Retrieval Augmented Generation (RAG)
    public string[]? AdditionalResources { get; set; } = null;
}
