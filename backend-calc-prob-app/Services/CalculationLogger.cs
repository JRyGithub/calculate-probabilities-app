using System.Text.Json;

namespace BackendCalcPropApp.Services;

public class CalculationLogger : ICalculationLogger
{
    private readonly string logFilePath;
    private readonly SemaphoreSlim semaphore;
    private readonly bool isValidLogger;

    public CalculationLogger(string? customLogDirectory = null)
    {
        var logsDirectory = customLogDirectory ?? Path.Combine(Directory.GetCurrentDirectory(), "logs");

        try
        {
            Directory.CreateDirectory(logsDirectory);
            this.logFilePath = Path.Combine(logsDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
            this.isValidLogger = true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException ||
                                   ex is DirectoryNotFoundException ||
                                   ex is IOException)
        {
            Console.WriteLine($"Warning: Could not create log directory '{logsDirectory}': {ex.Message}");
            this.logFilePath = string.Empty;
            this.isValidLogger = false;
        }

        this.semaphore = new SemaphoreSlim(1, 1);
    }

    public async Task LogCalculationAsync(string calculationType, object inputs, object result, bool? isError = false)
    {
        if (!this.isValidLogger)
        {
            return;
        }

        var logEntry = new
        {
            Date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"),
            IsError = isError == true ? "ERROR" : "VALID",
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
        catch (Exception ex) when (ex is UnauthorizedAccessException ||
                                   ex is DirectoryNotFoundException ||
                                   ex is IOException)
        {
            // Handle file write errors gracefully - don't break the application
            Console.WriteLine($"Warning: Could not write to log file '{this.logFilePath}': {ex.Message}");
        }
        finally
        {
            this.semaphore.Release();
        }
    }
}
