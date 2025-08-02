using ClampingDevice.DTOs;
using ClampingDevice.Entities;

namespace ClampingDevice.Data;

public interface IDeviceRepository
{
    Task<Device?> GetByIdAsync(int deviceId);
    Task<DeviceDto?> GetBySerialNumberAsync(string serialNumber);
    Task AddAsync(Device device);
    void Update(Device device);
    void Delete(Device device);
    Task<List<Device>> GetAllDevicesAsync();
    Task<bool> SaveChangesAsync();
    Task<Device?> GetEntityBySerialNumberAsync(string serialNumber); // ← return entity (not DTO)

}
