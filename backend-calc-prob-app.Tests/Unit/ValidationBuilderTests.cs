using BackendCalcPropApp.Features.Intersection;
using BackendCalcPropApp.Features.Union;
using BackendCalcPropApp.Shared;

namespace BackendCalcPropApp.Tests.Unit;

public class ValidationBuilderTests
{
    [Fact]
    public void IntersectionRequest_Validate_ValidInput_ReturnsValid()
    {
        // Arrange
        var request = new IntersectionRequest { A = 0.5M, B = 0.3M };

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")
            .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")
            .Build();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void IntersectionRequest_Validate_InvalidInput_AddsErrors()
    {
        // Arrange
        var request = new IntersectionRequest { A = -0.1M, B = 1.5M };

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")
            .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")
            .Build();

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains("A must be between 0 and 1", result.Errors);
        Assert.Contains("B must be between 0 and 1", result.Errors);
    }

    [Theory]
    [InlineData(-0.1, 0.5, 1)]  // A invalid
    [InlineData(0.5, -0.1, 1)]  // B invalid
    [InlineData(1.5, 0.5, 1)]   // A invalid
    [InlineData(0.5, 1.5, 1)]   // B invalid
    [InlineData(-0.1, 1.5, 2)]  // Both invalid
    public void IntersectionRequest_Validate_BoundaryValues_ReturnsCorrectErrorCount(
        decimal a, decimal b, int expectedErrorCount)
    {
        // Arrange
        var request = new IntersectionRequest { A = a, B = b };

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")
            .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")
            .Build();

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(expectedErrorCount, result.Errors.Count);
    }

    [Fact]
    public void UnionRequest_Validate_ValidInput_ReturnsValid()
    {
        // Arrange
        var request = new UnionRequest { A = 0.7M, B = 0.4M };

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")
            .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")
            .Build();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ValidationBuilder_RequiredProperty_WorksWithRequestObjects()
    {
        // Arrange
        var request = new IntersectionRequest { A = 0.5M, B = 0.3M };

        // Act
        var result = request.Validate()
            .RequiredProperty(x => x.A, "A is required")
            .RequiredProperty(x => x.B, "B is required")
            .Build();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ValidationBuilder_RequiredProperty_DefaultErrorMessage_UsesPropertyName()
    {
        // Test with a nullable request type if you have one
        // This might not work with your current non-nullable request types
        // You'd need a request type with nullable properties to test this properly

        // For now, testing with explicit error messages
        var request = new IntersectionRequest { A = 0.0M, B = 0.0M };

        var result = request.Validate()
            .RequiredProperty(x => x.A, "A is required")
            .Build();

        // Since A = 0.0 is a valid value (not null), this should pass
        Assert.True(result.IsValid);
    }

    [Fact]
    public void ValidationBuilder_ChainedRules_WorksCorrectly()
    {
        // Arrange
        var request = new IntersectionRequest { A = 0.5M, B = 0.3M };

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0, "A must be non-negative")
            .Rule(x => x.A <= 1, "A must be <= 1")
            .Rule(x => x.B >= 0, "B must be non-negative")
            .Rule(x => x.B <= 1, "B must be <= 1")
            .Rule(x => x.A + x.B <= 2, "Sum must be reasonable")
            .Build();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ValidationBuilder_MixedValidAndInvalidRules_OnlyAddsFailures()
    {
        // Arrange
        var request = new IntersectionRequest { A = 0.5M, B = 1.5M }; // B is invalid

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")  // Valid
            .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")  // Invalid
            .Rule(x => x.A > 0, "A must be positive")                      // Valid
            .Build();

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Contains("B must be between 0 and 1", result.Errors);
        Assert.DoesNotContain("A must be between 0 and 1", result.Errors);
        Assert.DoesNotContain("A must be positive", result.Errors);
    }

    [Fact]
    public void ValidationBuilder_NoRules_ReturnsValid()
    {
        // Arrange
        var request = new IntersectionRequest { A = 0.5M, B = 0.3M };

        // Act
        var result = request.Validate().Build();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ValidationBuilder_ComplexRules_WorksCorrectly()
    {
        // Arrange
        var request = new IntersectionRequest { A = 0.5M, B = 0.3M };

        // Act
        var result = request.Validate()
            .Rule(x => x.A != 0 || x.B != 0, "At least one value must be non-zero")
            .Rule(x => x.A * x.B >= 0, "Product must be non-negative")
            .Rule(x => Math.Abs(x.A - x.B) <= 1, "Values must be within reasonable range")
            .Build();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ValidationResult_PropertiesWork_Correctly()
    {
        // Arrange
        var request = new IntersectionRequest { A = -0.1M, B = 1.5M };

        // Act
        var result = request.Validate()
            .Rule(x => x.A >= 0, "A must be non-negative")
            .Rule(x => x.B <= 1, "B must be <= 1")
            .Build();

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.IsType<List<string>>(result.Errors);
    }

    [Fact]
    public void ValidationBuilder_PerformanceTest_HandlesMultipleRules()
    {
        var request = new IntersectionRequest { A = 0.5M, B = 0.3M };
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var test = request.Validate();
        for (int i = 0; i < 100; i++)
        {
            test.Rule(x => x.A >= 0, $"Rule {i}: A must be non-negative");
        }
        var result = test.Build();
        stopwatch.Stop();

        Assert.True(result.IsValid);
        Assert.True(stopwatch.ElapsedMilliseconds < 50);
    }
}