using OpenAI.Chat;
using System.ClientModel;


namespace IntellegenceService.Models;

public class QueryCompletionResult
{
    public string? Message { get; set; } = null;

    public int StatusCode { get; set; }

    public ClientResult<ChatCompletion>? GeneratedContent {get; set;} = null;
}
