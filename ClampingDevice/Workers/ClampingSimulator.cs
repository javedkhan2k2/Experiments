
using ClampingDevice.Services;

namespace ClampingDevice.Workers;

public class ClampingSimulator(IServiceProvider services) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IClampingDataService>();
            var result = await service.AddFakeClampingAsync();
            if (result.IsFailure)
            {
                // Log the error
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ClampingSimulator>>();
                logger.LogError(result.Error.Message);
            }
            else
            {
                // Optionally log success
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ClampingSimulator>>();
                logger.LogInformation("Successfully simulated clamping data.");
            }
            // Call the service to simulate clamping data
            await Task.Delay(5000, stoppingToken); // Every 5s
        }
    }
}
