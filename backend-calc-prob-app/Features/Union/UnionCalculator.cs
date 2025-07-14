namespace BackendCalcPropApp.Features.Union;

public class UnionCalculator : IUnionCalculator
{
    public decimal CalculateUnion(decimal a, decimal b)
    {
        return a + b - (a * b);
    }
}
