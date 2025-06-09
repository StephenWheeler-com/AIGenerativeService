using IntellegenceService.Models;

namespace WTI.InsightDaily.UX.CognitiveApiNS.Interfaces;

public interface ICognitiveServices
{
    Task<QueryCompletionResult> GenerateContent(QueryRequest request);
}
