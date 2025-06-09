namespace IntellegenceService.Models;

public class QueryDeployment
{
    public string EndPointUrl { get; set; }

    public string ModelName { get; set; }

    public string ApiKey { get; set; }

    public string ResponseFormat { get; set; }
}
