using Bogus;
using ClampingDevice.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

public class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Devices.AnyAsync()) return; // Skip if data exists

        var deviceFaker = new Faker<Device>()
            .RuleFor(d => d.SerialNumber, f => $"SN-{f.Random.AlphaNumeric(6).ToUpper()}")
            .RuleFor(d => d.Model, f => f.PickRandom("MX-100", "TX-200", "GX-500"))
            .RuleFor(d => d.Location, f => f.Address.City())
            .RuleFor(d => d.IsActive, f => f.Random.Bool(0.8f))
            .RuleFor(d => d.RegisteredAt, f => f.Date.Past(1))
            .RuleFor(d => d.LastUpdatedAt, f => f.Date.Recent(10));

        var devices = deviceFaker.Generate(10);
        await context.Devices.AddRangeAsync(devices);
        await context.SaveChangesAsync();

        var clampingFaker = new Faker<ClampingData>()
            .RuleFor(c => c.DeviceId, f => f.PickRandom(devices).Id)
            .RuleFor(c => c.ClampingForceN, f => f.Random.Double(500, 1500))
            .RuleFor(c => c.TemperatureC, f => f.Random.Double(20, 100))
            .RuleFor(c => c.Timestamp, f => f.Date.Recent(30))
            .RuleFor(c => c.IsValid, f => f.Random.Bool(0.9f))
            .RuleFor(c => c.WarningMessage, (f, c) => !c.IsValid ? f.Lorem.Sentence(3) : null)
            .RuleFor(c => c.IsDeleted, f => false)
            .RuleFor(c => c.ActionType, f => f.PickRandom<ClampingActionType>());

        var clampings = clampingFaker.Generate(100);
        await context.ClampingsData.AddRangeAsync(clampings);

        var eventLogFaker = new Faker<EventLog>()
            .RuleFor(e => e.EventType, f => f.PickRandom("Info", "Warning", "Error"))
            .RuleFor(e => e.Message, f => f.Lorem.Sentence(6))
            .RuleFor(e => e.Timestamp, f => f.Date.Recent(15));

        var logs = eventLogFaker.Generate(40);
        await context.EventLogs.AddRangeAsync(logs);

        await context.SaveChangesAsync();
    }
}
