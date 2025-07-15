using System.Linq.Expressions;

namespace BackendCalcPropApp.Shared.Interfaces;

public interface IValidationBuilder<T>
{
    IValidationBuilder<T> Rule(Func<T, bool> predicate, string errorMessage);

    IValidationBuilder<T> RequiredProperty<TProperty>(
        Expression<Func<T, TProperty>> propertyExpression,
        string errorMessage = "");

    ValidationResult Build();
}
