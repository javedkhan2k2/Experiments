using AutoMapper;
using ClampingDevice.Common.Results;
using ClampingDevice.Data;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;

namespace ClampingDevice.Services;

public class ClampingDataService(IClampingDataRepository clampingDataRepository, IDeviceRepository deviceRepository, IMapper mapper, ILogger<ClampingDataService> logger) : BaseService(logger), IClampingDataService
{
    public async Task<Result<ClampingDataDto>> CreateAsync(CreateClampingDataDto dto)
    {
        if (dto is null) return Result.Failure<ClampingDataDto>(new Error("ClampingDataServiceCreateError", "The dto cannot be null"));
        if( string.IsNullOrWhiteSpace(dto.serialNumber))
            return Result.Failure<ClampingDataDto>(new Error("ClampingDataServiceCreateError", "The serial number cannot be null or empty"));

        return await TryExecuteAsync(async () =>
        {
            // Check if device exists
            var existingDevice = await deviceRepository.GetBySerialNumberAsync(dto.serialNumber);
            if (existingDevice is null) return Result.Failure<ClampingDataDto>(new Error("DeviceNotFound", "The specified device does not exist"));

            var warnings = new List<string>();
            if (dto.ClampingForceN < 500 || dto.ClampingForceN > 1500)
                warnings.Add($"Clamping force {dto.ClampingForceN}N is out of range.");
            if (dto.TemperatureC < 10 || dto.TemperatureC > 60) 
                warnings.Add($"Temperature {dto.TemperatureC}C is out of range.");

            // Map DTO to entity
            var entity = mapper.Map<ClampingData>(dto);
            entity.Timestamp = DateTime.UtcNow; // Set the timestamp to the current time
            entity.IsValid = !warnings.Any();
            entity.WarningMessage = string.Join(" | ", warnings);
            entity.DeviceId = existingDevice.Id; // Set the DeviceId from the existing device
            // Save the entity
            await clampingDataRepository.AddAsync(entity);
            if (await clampingDataRepository.SaveChangesAsync())
                return Result.Success(mapper.Map<ClampingDataDto>(entity));
            return Result.Failure<ClampingDataDto>(new Error("SaveError", "Failed to save the clamping data"));
        }, nameof(CreateAsync));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        if(id < 1) return Result.Failure(new Error("ClampingDataServiceDeleteError", "The id cannot be less than 1"));
        return await TryExecuteAsync(async () =>
        {
            var entity = await clampingDataRepository.GetByIdAsync(id);
            if (entity is null) return Result.Failure(new Error("ClampingDataNotFound", "The specified clamping data does not exist"));
            entity.IsDeleted = true; // Mark the entity as deleted
            clampingDataRepository.Update(entity);
            if (await clampingDataRepository.SaveChangesAsync())
                return Result.Success();
            return Result.Failure(new Error("SaveError", "Failed to delete the clamping data"));
        }, nameof(DeleteAsync));
    }

    public async Task<Result<IEnumerable<ClampingDataDto>>> GetAllAsync()
    {
        return await TryExecuteAsync(async () =>
        {
            var entities = await clampingDataRepository.GetAllAsync();
            if (entities is null || !entities.Any())
                return Result.Success(Enumerable.Empty<ClampingDataDto>());
            return Result.Success(mapper.Map<IEnumerable<ClampingDataDto>>(entities));
        }, nameof(GetAllAsync));
    }

    public async Task<Result<IEnumerable<ClampingDataDto>>> GetByDeviceSerialAsync(string serialNumber)
    {
        if(string.IsNullOrWhiteSpace(serialNumber)) return Result.Failure<IEnumerable<ClampingDataDto>>(new Error("ClampingDataServiceGetByDeviceSerialError", "The serial number cannot be null or empty"));

        return await TryExecuteAsync(async () =>
        {
            var device = await deviceRepository.GetBySerialNumberAsync(serialNumber);
            if (device is null) return Result.Failure<IEnumerable<ClampingDataDto>>(new Error("DeviceNotFound", "The specified device does not exist"));
            var entities = await clampingDataRepository.GetByDeviceIdAsync(device.Id);
            if (entities is null || !entities.Any())
                return Result.Success(Enumerable.Empty<ClampingDataDto>());
            return Result.Success(mapper.Map<IEnumerable<ClampingDataDto>>(entities));
        }, nameof(GetByDeviceSerialAsync));
    }

    public async Task<Result<ClampingDataDto>> GetByIdAsync(int id)
    {
        if(id < 1) return Result.Failure<ClampingDataDto>(new Error("ClampingDataServiceGetByIdError", "The id cannot be less than 1"));
        return await TryExecuteAsync(async () =>
        {
            var entity = await clampingDataRepository.GetByIdAsync(id);
            if (entity is null) return Result.Failure<ClampingDataDto>(new Error("NotFound", "The specified clamping data does not exist"));
            return Result.Success(mapper.Map<ClampingDataDto>(entity));
        }, nameof(GetByIdAsync));
    }
}
