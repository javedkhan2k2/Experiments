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

}
