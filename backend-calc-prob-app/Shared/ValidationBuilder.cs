using System.Linq.Expressions;
using BackendCalcPropApp.Shared.Interfaces;

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

    public IValidationBuilder<T> RequiredProperty<TProperty>(
        Expression<Func<T, TProperty>> propertyExpression,
        string errorMessage = "")
    {
        var propertyName = GetPropertyName(propertyExpression);
        return this.Rule(
            item => propertyExpression.Compile()(item) != null,
            string.IsNullOrEmpty(errorMessage) ? $"{propertyName} is required" : errorMessage);
    }

    public ValidationResult Build() => new(this.errors.Count == 0, this.errors);

    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        if (propertyExpression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        throw new ArgumentException("Expression must be a property access");
    }
}
