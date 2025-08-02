using ClampingDevice.DTOs;
using ClampingDevice.Entities;

namespace ClampingDevice.Data;

public interface IDeviceRepository
{
    Task<Device?> GetByIdAsync(int deviceId);
    Task<Device?> GetBySerialNumberAsync(string serialNumber);
    Task AddAsync(Device device);
    void Update(Device device);
    void Delete(Device device);
    Task<List<Device>> GetAllDevicesAsync();
    Task<bool> SaveChangesAsync();

}
