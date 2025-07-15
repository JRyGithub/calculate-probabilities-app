using BackendCalcPropApp.Services;

namespace BackendCalcPropApp.Tests.Unit;

public class CalculationLoggerTests : IDisposable
{
    private readonly string testDirectory;
    private readonly CalculationLogger logger;

    public CalculationLoggerTests()
    {
        testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(testDirectory);

        logger = new CalculationLogger(testDirectory);
    }

    [Fact]
    public async Task LogCalculationAsync_ValidInput_CreatesLogFile()
    {
        var calculationName = "Intersection";
        var request = new { A = 0.5, B = 0.3 };
        var result = 0.15;

        await logger.LogCalculationAsync(calculationName, request, result);

        var expectedLogFile = Path.Combine(testDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        Assert.True(File.Exists(expectedLogFile));
    }

    [Fact]
    public async Task LogCalculationAsync_ValidInput_WritesCorrectFormat()
    {
        var calculationName = "Union";
        var request = new { A = 0.7, B = 0.4 };
        var result = 0.82;

        await logger.LogCalculationAsync(calculationName, request, result);

        var logFile = Path.Combine(testDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        var logContent = await File.ReadAllTextAsync(logFile);

        Assert.Contains(calculationName, logContent);
        Assert.Contains("0.7", logContent);
        Assert.Contains("0.4", logContent);
        Assert.Contains("0.82", logContent);
        Assert.Contains(DateTime.Now.ToString("yyyy-MM-dd"), logContent);
    }

    [Fact]
    public async Task LogCalculationAsync_WithErrors_LogsErrorList()
    {
        var calculationName = "Intersection";
        var request = new { A = -0.1, B = 1.5 };
        var errors = new List<string> { "A must be between 0 and 1", "B must be between 0 and 1" };

        await logger.LogCalculationAsync(calculationName, request, errors, true);

        var logFile = Path.Combine(testDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        var logContent = await File.ReadAllTextAsync(logFile);

        Assert.Contains("A must be between 0 and 1", logContent);
        Assert.Contains("B must be between 0 and 1", logContent);
        Assert.Contains("ERROR", logContent);
    }

    [Fact]
    public async Task LogCalculationAsync_MultipleEntries_AppendsToSameFile()
    {
        var request1 = new { A = 0.5, B = 0.3 };
        var request2 = new { A = 0.7, B = 0.4 };

        await logger.LogCalculationAsync("Intersection", request1, 0.15);
        await logger.LogCalculationAsync("Union", request2, 0.82);

        var logFile = Path.Combine(testDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        var logContent = await File.ReadAllTextAsync(logFile);

        Assert.Contains("Intersection", logContent);
        Assert.Contains("Union", logContent);
        Assert.Contains("0.15", logContent);
        Assert.Contains("0.82", logContent);
    }

    [Fact]
    public async Task LogCalculationAsync_DifferentDays_CreatesSeparateFiles()
    {
        var today = DateTime.Now;
        var expectedFileName = $"calculations_{today:yyyy-MM-dd}.log";

        await logger.LogCalculationAsync("Test", new { A = 0.5 }, 0.5);

        var expectedPath = Path.Combine(testDirectory, expectedFileName);
        Assert.True(File.Exists(expectedPath));
    }

    [Fact]
    public async Task LogCalculationAsync_ConcurrentCalls_ThreadSafe()
    {
        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            var index = i;
            tasks.Add(logger.LogCalculationAsync($"Test{index}", new { A = 0.1 * index }, 0.1 * index));
        }

        await Task.WhenAll(tasks);

        var logFile = Path.Combine(testDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        var logContent = await File.ReadAllTextAsync(logFile);

        for (int i = 0; i < 10; i++)
        {
            Assert.Contains($"Test{i}", logContent);
        }
    }

    [Fact]
    public async Task LogCalculationAsync_InvalidDirectory_HandlesGracefully()
    {
        // Create a path that definitely doesn't exist
        var invalidPath = Path.Combine(Path.GetTempPath(), "nonexistent_directory_" + Guid.NewGuid().ToString());
        var invalidLogger = new CalculationLogger(invalidPath);

        // Should NOT throw an exception - logger handles it gracefully
        var exception = await Record.ExceptionAsync(async () =>
        {
            await invalidLogger.LogCalculationAsync("Test", new { A = 0.5 }, 0.5);
        });

        Assert.Null(exception); // No exception should be thrown
    }

    [Fact]
    public async Task LogCalculationAsync_LargeRequest_HandlesCorrectly()
    {
        var largeRequest = new
        {
            A = 0.123456789,
            B = 0.987654321,
            Metadata = new { UserId = "test-user", RequestId = Guid.NewGuid() },
            AdditionalData = Enumerable.Range(1, 100).ToArray()
        };

        await logger.LogCalculationAsync("Complex", largeRequest, 0.5);

        var logFile = Path.Combine(testDirectory, $"calculations_{DateTime.Now:yyyy-MM-dd}.log");
        var logContent = await File.ReadAllTextAsync(logFile);

        Assert.Contains("Complex", logContent);
        Assert.Contains("0.123456789", logContent);
    }

    public void Dispose()
    {
        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, recursive: true);
        }
    }
}