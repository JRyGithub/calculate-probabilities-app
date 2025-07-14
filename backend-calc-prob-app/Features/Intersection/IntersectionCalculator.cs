namespace BackendCalcPropApp.Features.Intersection;

public class IntersectionCalculator : IIntersectionCalculator
{
    public decimal CalculateIntersection(decimal a, decimal b)
    {
        return a * b;
    }
}
