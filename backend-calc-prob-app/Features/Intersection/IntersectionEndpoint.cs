using BackendCalcPropApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendCalcPropApp.Features.Intersection;

public static class IntersectionEndpoint
{
    private const string CalculationName = "Intersection";

    public static void MapIntersectionEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/intersection", async ([FromBody] IntersectionRequest request, IIntersectionCalculator calculator, ICalculationLogger logger) =>
        {
            var validationResult = request.Validate()
                .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")
                .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")
                .Build();

            if (!validationResult.IsValid)
            {
                await logger.LogCalculationAsync(CalculationName, request, validationResult.Errors);
                return Results.BadRequest(validationResult.Errors);
            }

            var result = calculator.CalculateIntersection(request.A, request.B);

            await logger.LogCalculationAsync(CalculationName, request, result);

            return Results.Ok(result);
        })
        .WithName("CalculateIntersection")
        .WithSummary("Calculate the intersection of two probabilities")
        .Produces<decimal>(200)
        .ProducesValidationProblem(400);
    }
}
