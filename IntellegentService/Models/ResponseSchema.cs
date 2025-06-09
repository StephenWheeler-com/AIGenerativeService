namespace IntellegenceService.Models;

public class ResponseSchema
{
    public string Type { get; set; }

    public string Properties { get; set; }

    public string[] Required { get; set; }

    public bool AdditionalProperties { get; set; }

    public string? Schema { get; set; }
}

