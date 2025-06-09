using IntellegenceService.Models;
using OpenAI.Chat;
using WTI.InsightDaily.UX.Reporting;

namespace WTI.InsightDaily.UX.DispatcherNS.Interfaces;

public interface IDispatcher
{
    Task<GenerativeTaskModel> GenerateContentAsync(QueryRequest request);
}
