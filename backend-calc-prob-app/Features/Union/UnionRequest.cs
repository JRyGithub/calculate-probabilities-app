using System.ComponentModel.DataAnnotations;

namespace BackendCalcPropApp.Features.Union;

public record UnionRequest()
{
    [Required]
    [Range(0.0, 1.0, ErrorMessage = "A must be between 0 and 1")]
    public decimal A { get; init; }

    [Required]
    [Range(0.0, 1.0, ErrorMessage = "B must be between 0 and 1")]
    public decimal B { get; init; }
}
