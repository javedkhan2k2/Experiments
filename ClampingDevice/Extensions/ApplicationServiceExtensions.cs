using ClampingDevice.Data;
using ClampingDevice.Services;
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
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Register Repositories and Services
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IDeviceService, DeviceService>();

        return services;
    }
}
