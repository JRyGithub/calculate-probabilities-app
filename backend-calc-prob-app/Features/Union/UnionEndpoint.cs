using BackendCalcPropApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendCalcPropApp.Features.Union;

public static class UnionEndpoint
{
    private const string CalculationName = "Union";

    public static void MapUnionEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/union", async ([FromBody] UnionRequest request, IUnionCalculator calculator, ICalculationLogger logger) =>
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

            var result = calculator.CalculateUnion(request.A, request.B);

            await logger.LogCalculationAsync(CalculationName, request, result);

            return Results.Ok(result);
        })
        .WithName("CalculateUnion")
        .WithSummary("Calculate the union of two probabilities")
        .Produces<decimal>(200)
        .ProducesValidationProblem(400);
    }
}
