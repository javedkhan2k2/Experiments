using ClampingDevice.Data;
using ClampingDevice.Services;
using ClampingDevice.Workers;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
           opt.UseSqlite("Data Source=clamps.db")
        );
        services.AddControllers();
        services.AddOpenApi();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddCors();
        services.AddHostedService<ClampingSimulator>();
        services.AddSignalR();

        // Register Repositories and Services
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IClampingDataRepository, ClampingDataRepository>();
        services.AddScoped<IEventLogRepository, EventLogRepository>();
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddScoped<IClampingDataService, ClampingDataService>();
        services.AddScoped<IEventLogService, EventLogService>();

        return services;
    }
}
