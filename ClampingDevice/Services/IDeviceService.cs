using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;

namespace ClampingDevice.Services;

public interface IDeviceService
{
    Task<Result<DeviceDto>> RegisterAsync(CreateDeviceDto deviceDto);
    Task<Result<IEnumerable<DeviceDto>>> GetAllAsync();
    Task<Result<DeviceDto>> GetBySerialNumberAsync(string serialNumber);
    Task<Result> UpdateAsync(string serialNumber, UpdateDeviceDto dto);
    Task<Result> DeleteAsync(string serialNumber);
    Task<Result> ToggleActiveAsync(string serialNumber);
}
