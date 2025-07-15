using BackendCalcPropApp.Features.Intersection;

namespace BackendCalcPropApp.Tests.Unit;

public class IntersectionCalculatorTests
{
    [Fact]
    public void CalculateIntersection_ValidInputs_ReturnsCorrectResult()
    {
        var calculator = new IntersectionCalculator();

        var result = calculator.CalculateIntersection(0.5M, 0.5M);

        Assert.Equal(0.25M, result);
    }

    [Theory]
    [InlineData(0.0, 0.5, 0.0)]
    [InlineData(1.0, 0.5, 0.5)]
    [InlineData(0.25, 0.8, 0.2)]
    public void CalculateIntersection_VariousInputs_ReturnsExpectedResults(
        decimal a, decimal b, decimal expected)
    {
        var calculator = new IntersectionCalculator();

        var result = calculator.CalculateIntersection(a, b);

        Assert.Equal(expected, result, precision: 10);
    }

    [Fact]
    public void CalculateUnion_ZeroInputs_ReturnsZero()
    {
        var calculator = new IntersectionCalculator();
        var result = calculator.CalculateIntersection(0.0M, 0.0M);
        Assert.Equal(0.0M, result);
    }
}