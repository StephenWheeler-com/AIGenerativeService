using Azure;
using Azure.AI.OpenAI;
using Demo.GenerativeAI.Common.Settings;
using Demo.GenerativeAI.KeyVaultNS.Interfaces;
using IntellegenceService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using WTI.InsightDaily.UX.CognitiveApiNS.Interfaces;

namespace WTI.InsightDaily.UX.CognitiveApiNS;

public class CognitiveServices : ICognitiveServices
{
    private readonly ILogger logger;

    private readonly IKeyVault keyVault;

    private readonly CognitiveServicesSettings settings = null;

    public CognitiveServices(IOptions<CognitiveServicesSettings> settingOptions, IKeyVault keyVault, ILogger<CognitiveServices> logger)
    {
        this.settings = settingOptions.Value;

        this.keyVault = keyVault;

        this.logger = logger;
    }

    public async Task<QueryCompletionResult> GenerateContent(QueryRequest request)
    {
        QueryCompletionResult? queryResult = null;

        CompletionOptions? querySettings = null;
        if(!settings.QueryOptions.TryGetValue(request.QueryName, out querySettings))
        {
            return new QueryCompletionResult
            {
                Message = "Unknown Query",
                StatusCode = 404
            };
        }

        var endpointUrl = new Uri(querySettings.EndPointUrl);

        var modelName = querySettings.ModelName;

        var apiKey = await keyVault.GetApiSecret();

        List<ChatMessage> prompts = new List<ChatMessage>();

        foreach (var prompt in request?.SystemPrompts)
        {
            prompts.Add(new SystemChatMessage(prompt));
        }

        foreach (var prompt in request?.UserPrompts)
        {
            prompts.Add(new UserChatMessage(prompt));
        }

        foreach (var resource in request?.AdditionalResources)
        {
        }

        try
        {
            AzureOpenAIClient azureClient = new(
                endpointUrl,
                new AzureKeyCredential(apiKey));

            var chatClient = azureClient.GetChatClient(modelName);

            ChatResponseFormat responseFormat = ChatResponseFormat.CreateTextFormat();
            
            // Initialize ChatResponseFormat object with JSON schema of desired response format.
            if (querySettings.StructedOutput != null)
            {
                var formatSchema = File.ReadAllText(querySettings.StructedOutput.SchemaFile);

                responseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: querySettings.StructedOutput.SchemaName,
                    jsonSchemaIsStrict: querySettings.StructedOutput.IsStrict,
                    jsonSchema: BinaryData.FromString(formatSchema));
            }

            var requestOptions = new ChatCompletionOptions()
            {
                MaxOutputTokenCount = querySettings.MaxOutputTokenCount,
                Temperature = querySettings.Temperature,
                TopP = querySettings.TopP,
                FrequencyPenalty = querySettings.FrequencyPenalty,
                PresencePenalty = querySettings.PresencePenalty,
                ResponseFormat = responseFormat
            };

            var generatedContent = await chatClient.CompleteChatAsync(prompts, requestOptions);

            queryResult = new QueryCompletionResult
            {
                Message = "Success",
                StatusCode = 200,
                GeneratedContent = generatedContent
            };
        }
        catch (Exception ex)
        {
            var msg = $"Exception: CognitiveServices::GenerateContent => Source={ex.Source} Message={ex.Message}";
            logger.LogError(msg);

            queryResult = new QueryCompletionResult
            {
                Message = "msg",
                StatusCode = 500
            };
        }

        return queryResult;
    }
}
