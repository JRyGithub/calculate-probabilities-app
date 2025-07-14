using BackendCalcPropApp.Features.Intersection;
using BackendCalcPropApp.Features.Union;

namespace BackendCalcPropApp.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapCalculatorEndpoints(this WebApplication app)
    {
        var calculatorGroup = app.MapGroup("/api/calculator")
            .WithTags("Calculator")
            .WithOpenApi();

        IntersectionEndpoint.MapIntersectionEndpoints(calculatorGroup);
        UnionEndpoint.MapUnionEndpoints(calculatorGroup);

        return app;
    }
}
