using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using ClampingDevice.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

public class DeviceRepository(AppDbContext dbContext, IMapper mapper) : IDeviceRepository
{
    public async Task AddAsync(Device device) => await dbContext.Devices.AddAsync(device);

    public void Delete(Device device) => dbContext.Devices.Remove(device);

    public async Task<PagedList<DeviceDto>> GetAllDevicesAsync(DeviceParams deviceParams)
    {
        var query = dbContext.Devices.AsQueryable();
        query = query.OrderByDescending(d => d.RegisteredAt) // Default ordering by CreatedAt
            .AsNoTracking(); // Use AsNoTracking for read-only queries
        // TODO: Add filtering, sorting, and searching logic based on deviceParams

        return await PagedList<DeviceDto>.CreateAsync(query.ProjectTo<DeviceDto>(mapper.ConfigurationProvider),
            deviceParams.PageNumber,
            deviceParams.PageSize);
    }

    public async Task<Device?> GetByIdAsync(int deviceId) => await dbContext.Devices.FindAsync(deviceId);

    public async Task<Device?> GetBySerialNumberAsync(string serialNumber)
    {
        return await dbContext.Devices
            .Where(d => d.SerialNumber == serialNumber)
            .FirstOrDefaultAsync();
    }

    public void Update(Device device) => dbContext.Devices.Update(device);

    public async Task<bool> SaveChangesAsync() => await dbContext.SaveChangesAsync() > 0;

    public async Task<DeviceStatsDto> GetStatsAsync()
    {
        // Original code commented out for performance reasons
        //var totalDevices = await dbContext.Devices.CountAsync();
        //var activeDevices = await dbContext.Devices.CountAsync(d => d.IsActive);
        //var inactiveDevices = totalDevices - activeDevices;
        //var uniqueModels = await dbContext.Devices.Select(d => d.Model).Distinct().CountAsync();

        //return new DeviceStatsDto
        //{
        //    TotalDevices = totalDevices,
        //    ActiveDevices = activeDevices,
        //    InactiveDevices = inactiveDevices,
        //    UniqueModels = uniqueModels
        //};

        var result = await dbContext.Devices
        .GroupBy(_ => 1)
        .Select(g => new DeviceStatsDto
        {
            TotalDevices = g.Count(),
            ActiveDevices = g.Count(d => d.IsActive),
            InactiveDevices = g.Count(d => !d.IsActive),
            UniqueModels = g.Select(d => d.Model).Distinct().Count()
        })
        .FirstOrDefaultAsync();

        return result ?? new DeviceStatsDto(); // handle empty case
    }

    public async Task<IEnumerable<Device>> GetLastFiveAsync()
    {
        return await dbContext.Devices
            .OrderByDescending(d => d.RegisteredAt)
            .Take(5)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Device> GetRandomDeviceAsync()
    {
        var activeDevices = await dbContext.Devices
            .Where(d => d.IsActive)
            .ToListAsync();

        var randomDevice = activeDevices
            .OrderBy(d => Guid.NewGuid())
            .FirstOrDefault();

        return randomDevice ?? new Device();

    }

}
