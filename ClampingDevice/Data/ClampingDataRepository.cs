using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

public class ClampingDataRepository(AppDbContext dbContext) : IClampingDataRepository
{
    public async Task AddAsync(ClampingData data) => await dbContext.ClampingsData.AddAsync(data);

    public void Update(ClampingData data) => dbContext.ClampingsData.Update(data);

    public async Task<IEnumerable<ClampingData>> GetAllAsync()
    {
        return await dbContext.ClampingsData
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<ClampingData>> GetByDeviceIdAsync(int deviceId)
    {
        return await dbContext.ClampingsData
            .Where(c => c.DeviceId == deviceId)
            .ToListAsync();
    }

    public async Task<ClampingData?> GetByIdAsync(int id)
    {
        return await dbContext.ClampingsData
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ClampingData?> GetLatestByDeviceIdAsync(int deviceId)
    {
        return await dbContext.ClampingsData
            .Where(c => c.DeviceId == deviceId)
            .OrderByDescending(c => c.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveChangesAsync() => await dbContext.SaveChangesAsync() > 0;

    public async Task<ClampingStatsDto> GetClampingStatsAsync()
    {
        var clampings = dbContext.ClampingsData.AsNoTracking();

        var totalClampings = await clampings.CountAsync(c => c.ActionType == ClampingActionType.Clamp);
        var totalUnclampings = await clampings.CountAsync(c => c.ActionType == ClampingActionType.Unclamp);

        var failedClampings = await clampings.CountAsync(c => c.ActionType == ClampingActionType.Clamp && !c.IsValid);

        var recentClampingTimestamp = await clampings
            .Where(c => c.ActionType == ClampingActionType.Clamp)
            .OrderByDescending(c => c.Timestamp)
            .Select(c => (DateTime?)c.Timestamp)
            .FirstOrDefaultAsync();

        var recentUnclampingTimestamp = await clampings
            .Where(c => c.ActionType == ClampingActionType.Unclamp)
            .OrderByDescending(c => c.Timestamp)
            .Select(c => (DateTime?)c.Timestamp)
            .FirstOrDefaultAsync();

        var devicesInvolved = await clampings
            .Select(c => c.DeviceId)
            .Distinct()
            .CountAsync();

        return new ClampingStatsDto
        {
            TotalClampings = totalClampings,
            TotalUnclampings = totalUnclampings,
            FailedClampings = failedClampings,
            RecentClampingTimestamp = recentClampingTimestamp,
            RecentUnclampingTimestamp = recentUnclampingTimestamp,
            DevicesInvolved = devicesInvolved
        };
    }

    public async Task<int> GetFailedClampingsLast24hAsync()
    {
        // TODO Add a DTO in future to return more detailed stats
        var since = DateTime.UtcNow.AddHours(-24);

        return await dbContext.ClampingsData
        .AsNoTracking()
        .CountAsync(c => c.ActionType == ClampingActionType.Clamp
                      && !c.IsValid
                      && c.Timestamp >= since);
    }
}
