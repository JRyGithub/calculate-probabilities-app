using BackendCalcPropApp.Shared.Interfaces;

namespace BackendCalcPropApp.Shared;

public class ValidationBuilder<T> : IValidationBuilder<T>
{
    private readonly T item;
    private readonly List<string> errors = new();

    public ValidationBuilder(T item) => this.item = item;

    public IValidationBuilder<T> Rule(Func<T, bool> predicate, string errorMessage)
    {
        if (!predicate(this.item))
        {
            this.errors.Add(errorMessage);
        }

        return this;
    }

    public ValidationResult Build() => new(this.errors.Count == 0, this.errors);
}
