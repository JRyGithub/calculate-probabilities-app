namespace BackendCalcPropApp.Shared.Interfaces;

public interface IValidationBuilder<T>
{
    IValidationBuilder<T> Rule(Func<T, bool> predicate, string errorMessage);

    ValidationResult Build();
}
