using ClampingDevice.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

public class DeviceRepository(AppDbContext dbContext) : IDeviceRepository
{
    public async Task AddAsync(Device device) => await dbContext.Devices.AddAsync(device);

    public void Delete(Device device) => dbContext.Devices.Remove(device);

    public async Task<List<Device>> GetAllDevicesAsync() => await dbContext.Devices.ToListAsync();

    public async Task<Device?> GetByIdAsync(int deviceId) => await dbContext.Devices.FindAsync(deviceId);

    public async Task<Device?> GetBySerialNumberAsync(string serialNumber)
    {
        return await dbContext.Devices
            .Where(d => d.SerialNumber == serialNumber)
            .FirstOrDefaultAsync();
    }

    public void Update(Device device) => dbContext.Devices.Update(device);

    public async Task<bool> SaveChangesAsync() => await dbContext.SaveChangesAsync() > 0;


}
