using System.Text.Json;

namespace BackendCalcPropApp.Services;

public class CalculationLogger : ICalculationLogger
{
    private readonly string logFilePath;
    private readonly SemaphoreSlim semaphore;

    public CalculationLogger()
    {
        var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        Directory.CreateDirectory(logsDirectory);

        this.logFilePath = Path.Combine(logsDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        this.semaphore = new SemaphoreSlim(1, 1);
    }

    public async Task LogCalculationAsync(string calculationType, object inputs, object result)
    {
        var logEntry = new
        {
            Date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"),
            Calculation = calculationType,
            Inputs = inputs,
            Result = result,
        };

        var logLine = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions
        {
            WriteIndented = false,
        });

        await this.semaphore.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(this.logFilePath, logLine + Environment.NewLine);
        }
        finally
        {
            this.semaphore.Release();
        }
    }
}
