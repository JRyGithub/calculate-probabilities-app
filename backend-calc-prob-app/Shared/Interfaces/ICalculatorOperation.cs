namespace BackendCalcPropApp.Shared.Interfaces;

public interface ICalculatorOperation
{
    string OperationName { get; }

    double Calculate(double a, double b);
}
