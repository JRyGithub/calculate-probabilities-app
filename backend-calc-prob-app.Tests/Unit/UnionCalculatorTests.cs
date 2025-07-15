using BackendCalcPropApp.Features.Union;

namespace BackendCalcPropApp.Tests.Unit;

public class UnionCalculatorTests
{
    [Fact]
    public void CalculateUnion_ValidInputs_ReturnsCorrectResult()
    {
        var calculator = new UnionCalculator();

        var result = calculator.CalculateUnion(0.5M, 0.5M);

        Assert.Equal(0.75M, result);
    }

    [Theory]
    [InlineData(0.0, 0.5, 0.5)]
    [InlineData(1.0, 0.5, 1)]
    [InlineData(0.25, 0.8, 0.85)]
    public void CalculateUnion_VariousInputs_ReturnsExpectedResults(
        decimal a, decimal b, decimal expected)
    {
        var calculator = new UnionCalculator();

        var result = calculator.CalculateUnion(a, b);

        Assert.Equal(expected, result, precision: 10);
    }

    [Fact]
    public void CalculateUnion_ZeroInputs_ReturnsZero()
    {
        var calculator = new UnionCalculator();
        var result = calculator.CalculateUnion(0.0M, 0.0M);
        Assert.Equal(0.0M, result);
    }
}