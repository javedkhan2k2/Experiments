using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;

namespace ClampingDevice.Services;

public interface IClampingDataService
{
    Task<Result<IEnumerable<ClampingDataDto>>> GetAllAsync();
    Task<Result<ClampingDataDto>> GetByIdAsync(int id);
    Task<Result<ClampingDataDto>> CreateAsync(CreateClampingDataDto dto);
    Task<Result> DeleteAsync(int id);
    Task<Result<IEnumerable<ClampingDataDto>>> GetByDeviceSerialAsync(string serialNumber);
    Task<Result<ClampingStatsDto>> GetStatsAsync();
    Task<Result<int>> GetFailedClampingsLast24hAsync();
    Task<Result> AddFakeClampingAsync();
}
