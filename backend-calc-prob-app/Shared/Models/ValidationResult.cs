public class ValidationResult
{
    public ValidationResult(bool isValid, IReadOnlyList<string> errors)
    {
        this.IsValid = isValid;
        this.Errors = errors;
    }

    public bool IsValid { get; }

    public IReadOnlyList<string> Errors { get; }
}
