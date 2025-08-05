using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;
using ClampingDevice.Helpers;

namespace ClampingDevice.Services;

public interface IDeviceService
{
    Task<Result<DeviceDto>> RegisterAsync(CreateDeviceDto deviceDto);
    Task<Result<PagedList<DeviceDto>>> GetAllAsync(DeviceParams deviceParams);
    Task<Result<DeviceDto>> GetBySerialNumberAsync(string serialNumber);
    Task<Result> UpdateAsync(string serialNumber, UpdateDeviceDto dto);
    Task<Result> DeleteAsync(string serialNumber);
    Task<Result> ToggleActiveAsync(string serialNumber);
    Task<Result<DeviceStatusDto>> GetStatusAsync(string serialNumber);
    Task<Result<DeviceStatsDto>> GetStatsAsync();
    Task<Result<IEnumerable<DeviceDto>>> GetLastFiveAsync();
}
