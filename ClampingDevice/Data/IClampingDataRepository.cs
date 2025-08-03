using ClampingDevice.Entities;

namespace ClampingDevice.Data;

public interface IClampingDataRepository
{
    Task<IEnumerable<ClampingData>> GetAllAsync();
    Task<ClampingData?> GetByIdAsync(int id);
    Task<IEnumerable<ClampingData>> GetByDeviceIdAsync(int deviceId);
    Task AddAsync(ClampingData data);
    void Update(ClampingData data);

    Task<ClampingData?> GetLatestByDeviceIdAsync(int deviceId);
    Task<bool> SaveChangesAsync();

}
