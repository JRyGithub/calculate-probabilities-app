using BackendCalcPropApp.Services;
using BackendCalcPropApp.Shared;
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
                .Rule(x => x.A != 0 || x.B != 0, "At least one of A or B must be non-zero")
                .Rule(x => x.A >= 0 && x.A <= 1, "A must be between 0 and 1")
                .Rule(x => x.B >= 0 && x.B <= 1, "B must be between 0 and 1")
                .Build();

            IntersectionResult intersectionResult = new IntersectionResult();

            if (!validationResult.IsValid)
            {
                await logger.LogCalculationAsync(CalculationName, request, validationResult.Errors, true);

                intersectionResult.Errors = [.. validationResult.Errors];

                return Results.BadRequest(intersectionResult);
            }

            var result = calculator.CalculateIntersection(request.A, request.B);

            await logger.LogCalculationAsync(CalculationName, request, result);

            CalculationResult calculationResult = new CalculationResult
            {
                Result = result,
                Calculation = CalculationName,
            };

            intersectionResult.Result = result;

            return Results.Ok(intersectionResult);
        })
        .WithName("CalculateIntersection")
        .WithSummary("Calculate the intersection of two probabilities")
        .Produces<IntersectionResult>(200)
        .ProducesValidationProblem(400);
    }
}
