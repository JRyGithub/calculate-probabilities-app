namespace BackendCalcPropApp.Services;

public interface ICalculationLogger
{
    Task LogCalculationAsync(string calculationType, object inputs, object result, bool? isError = false);
}
