using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using ClampingDevice.Helpers;

namespace ClampingDevice.Data;

public interface IDeviceRepository
{
    Task<Device?> GetByIdAsync(int deviceId);
    Task<Device?> GetBySerialNumberAsync(string serialNumber);
    Task AddAsync(Device device);
    void Update(Device device);
    void Delete(Device device);
    Task<PagedList<DeviceDto>> GetAllDevicesAsync(DeviceParams deviceParams);
    Task<bool> SaveChangesAsync();
    Task<DeviceStatsDto> GetStatsAsync();
    Task<IEnumerable<Device>> GetLastFiveAsync();
    Task<Device> GetRandomDeviceAsync();
}
