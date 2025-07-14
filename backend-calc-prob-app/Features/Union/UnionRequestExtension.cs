using BackendCalcPropApp.Shared;

namespace BackendCalcPropApp.Features.Union;

public static class UnionRequestExtension
{
    public static ValidationBuilder<UnionRequest> Validate(this UnionRequest request)
        => new ValidationBuilder<UnionRequest>(request);
}
