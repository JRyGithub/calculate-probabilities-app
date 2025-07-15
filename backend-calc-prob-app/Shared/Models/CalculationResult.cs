public class CalculationResult
{
    public string Calculation { get; init; } = string.Empty;

    public decimal? Result { get; set; }

    public List<string>? Errors { get; set; } = new List<string>();
}
