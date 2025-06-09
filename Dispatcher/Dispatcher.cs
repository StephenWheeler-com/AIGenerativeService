using IntellegenceService.Models;
using WTI.InsightDaily.UX.CognitiveApiNS.Interfaces;
using WTI.InsightDaily.UX.DispatcherNS.Interfaces;
using WTI.InsightDaily.UX.Reporting;

namespace WTI.InsightDaily.UX.DispatcherNS;

public class Dispatcher : IDispatcher
{
    private readonly ICognitiveServices cognitiveServices;

    public Dispatcher(ICognitiveServices cognitiveServices)
    {
        this.cognitiveServices = cognitiveServices;
    }

    public async Task<GenerativeTaskModel> GenerateContentAsync(QueryRequest request)
    {
        GenerativeTaskModel queryResponse = null!;

        queryResponse = new GenerativeTaskModel
        {
            ReturnCode = 200
        };

        var q = await cognitiveServices.GenerateContent(request);

        foreach(var item in q.GeneratedContent.Value.Content)
        {
            queryResponse.Content?.Add(item.Text);
        }

        return queryResponse;
    }
}
