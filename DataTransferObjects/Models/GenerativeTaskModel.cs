namespace WTI.InsightDaily.UX.Reporting;

public class GenerativeTaskModel
{
    public GenerativeTaskModel()
    {
        Content = new List<string>();
    }

    public List<string>? Content { get; set; } = null!;

    public int ReturnCode { get; set; }
}
