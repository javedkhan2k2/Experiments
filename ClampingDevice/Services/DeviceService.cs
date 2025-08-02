using AutoMapper;
using ClampingDevice.Common.Results;
using ClampingDevice.Data;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using System.Net;

namespace ClampingDevice.Services;

public class DeviceService(IDeviceRepository deviceRepository, ILogger<DeviceService> logger, IMapper mapper) : BaseService(logger), IDeviceService
{
    public async Task<Result> DeleteAsync(string serialNumber)
    {
        if(string.IsNullOrWhiteSpace(serialNumber)) return Result.Failure(new Error("DeleteDeviceError", "Serial number cannot be null or empty."));

        return await TryExecuteAsync(async () =>
        {
            var device = await deviceRepository.GetEntityBySerialNumberAsync(serialNumber);
            if (device is null) return Result.Failure(new Error("DeviceNotFound", "Device with the specified serial number not found."));
            
            deviceRepository.Delete(device);
            if (await deviceRepository.SaveChangesAsync())
                return Result.Success();
            return Result.Failure(new Error("DeleteDeviceError", "Failed to delete the device."));
        }, nameof(DeleteAsync));
    }

    public async Task<Result<IEnumerable<DeviceDto>>> GetAllAsync()
    {
        return await TryExecuteAsync(async () =>
        {
            var devices = await deviceRepository.GetAllDevicesAsync();
            if (devices is null || !devices.Any()) return Result.Success(Enumerable.Empty<DeviceDto>());

            return Result.Success(mapper.Map<IEnumerable<DeviceDto>>(devices));

        }, nameof(GetAllAsync));
    }

    public async Task<Result<DeviceDto>> GetBySerialNumberAsync(string serialNumber)
    {
        if(string.IsNullOrWhiteSpace(serialNumber))
            return Result.Failure<DeviceDto>(new Error("GetDeviceError", "Serial number cannot be null or empty."));

        return await TryExecuteAsync(async () =>
        {
            var device = await deviceRepository.GetEntityBySerialNumberAsync(serialNumber);
            if (device is null) return Result.Failure<DeviceDto>(new Error("DeviceNotFound", "Device with the specified serial number not found."));

            return Result.Success(mapper.Map<DeviceDto>(device));
        }, nameof(GetBySerialNumberAsync));
    }

    public async Task<Result<DeviceDto>> RegisterAsync(CreateDeviceDto deviceDto)
    {
        if (deviceDto is null) return Result.Failure<DeviceDto>(new Error("RegisterDeviceError", "Device DTO cannot be null."));
        if (string.IsNullOrWhiteSpace(deviceDto.SerialNumber))
            return Result.Failure<DeviceDto>(new Error("RegisterDeviceError", "Serial number cannot be null or empty."));

        return await TryExecuteAsync(async () =>
        {
            // Check if the device already registered
            var existingDevice = await deviceRepository.GetBySerialNumberAsync(deviceDto.SerialNumber);
            if(existingDevice is not null) return Result.Failure<DeviceDto>(new Error("DeviceAlreadyRegistered", "A device with this serial number is already registered."));
            
            var device = mapper.Map<Device>(deviceDto);
            await deviceRepository.AddAsync(device);

            if(await deviceRepository.SaveChangesAsync()) return Result.Success(mapper.Map<DeviceDto>(device));

            return Result.Failure<DeviceDto>(new Error("RegisterDeviceError", "Failed to register the device."));
        }, nameof(RegisterAsync));
    }

    public async Task<Result> ToggleActiveAsync(string serialNumber)
    {
        if(string.IsNullOrWhiteSpace(serialNumber))
            return Result.Failure(new Error("ToggleActiveError", "Serial number cannot be null or empty."));

        return await TryExecuteAsync(async () =>
        {
            var device = await deviceRepository.GetEntityBySerialNumberAsync(serialNumber);
            if (device is null) return Result.Failure(new Error("DeviceNotFound", "Device with the specified serial number not found."));
            device.IsActive = !device.IsActive; // Toggle the active status
            deviceRepository.Update(device);
            if (await deviceRepository.SaveChangesAsync())
                return Result.Success();
            return Result.Failure(new Error("ToggleActiveError", "Failed to toggle the active status of the device."));
        }, nameof(ToggleActiveAsync));
    }

    public async Task<Result> UpdateAsync(string serialNumber, UpdateDeviceDto dto)
    {
        if(string.IsNullOrWhiteSpace(serialNumber)) return Result.Failure(new Error("UpdateDeviceError", "Serial number cannot be null or empty."));
        if(dto is null) return Result.Failure(new Error("UpdateDeviceError", "Update DTO cannot be null."));

        return await TryExecuteAsync(async () =>
        {
            var device = await deviceRepository.GetEntityBySerialNumberAsync(serialNumber);
            if (device is null) return Result.Failure(new Error("DeviceNotFound", "Device with the specified serial number not found."));
            // Update the device properties
            mapper.Map(dto, device);
            deviceRepository.Update(device);
            if (await deviceRepository.SaveChangesAsync())
                return Result.Success();
            return Result.Failure(new Error("UpdateDeviceError", "Failed to update the device."));
        }, nameof(UpdateAsync));
    }

}
