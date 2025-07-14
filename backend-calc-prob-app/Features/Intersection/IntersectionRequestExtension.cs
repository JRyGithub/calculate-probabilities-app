using BackendCalcPropApp.Shared;

namespace BackendCalcPropApp.Features.Intersection;

public static class IntersectionRequestExtension
{
    public static ValidationBuilder<IntersectionRequest> Validate(this IntersectionRequest request)
        => new ValidationBuilder<IntersectionRequest>(request);
}
