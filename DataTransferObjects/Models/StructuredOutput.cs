namespace IntellegenceService.Models;

public class StructuredOutput
{
    public string? SchemaName { get; set; } = null;

    public bool IsStrict { get; set; }

    public string? SchemaFile { get; set; } = null;
}
