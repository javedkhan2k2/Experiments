using AutoMapper;
using ClampingDevice.Common.Results;
using ClampingDevice.Data;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;

namespace ClampingDevice.Services;

public class EventLogService(IEventLogRepository eventLogRepository, IMapper mapper, ILogger<EventLogService> logger) : BaseService(logger), IEventLogService
{
    public async Task<Result<EventLogDto>> CreateAsync(CreateEventLogDto dto)
    {
        if(dto is null) return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "CreateEventLogDto cannot be null."));
        if(string.IsNullOrWhiteSpace(dto.EventType)) return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "EventType cannot be null or empty."));
        if(string.IsNullOrWhiteSpace(dto.Message)) return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "Message cannot be null or empty."));

        return await TryExecuteAsync(async () =>
        {
            var eventLog = mapper.Map<EventLog>(dto);
            eventLog.Timestamp = DateTime.UtcNow;
            await eventLogRepository.AddAsync(eventLog);
            if(await eventLogRepository.SaveChangesAsync()) return Result.Success(mapper.Map<EventLogDto>(eventLog));
            return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "Failed to save the event log."));
        }, nameof(CreateAsync));
    }

    public async Task<Result<IEnumerable<EventLogDto>>> GetAllAsync()
    {
        return await TryExecuteAsync(async () =>
        {
            var eventLogs = await eventLogRepository.GetAllAsync();
            
            if(eventLogs is null || !eventLogs.Any()) return Result.Success(Enumerable.Empty<EventLogDto>());
            
            return Result.Success(mapper.Map<IEnumerable<EventLogDto>>(eventLogs));
        }, nameof(GetAllAsync));
    }

    public async Task<Result<EventLogDto>> GetByIdAsync(int id)
    {
        if(id < 1) return Result.Failure<EventLogDto>(new Error("EventLogGetError", "Id must be greater than zero."));
        return await TryExecuteAsync(async () =>
        {
            var eventLog = await eventLogRepository.GetByIdAsync(id);
            if(eventLog is null) return Result.Failure<EventLogDto>(new Error("NotFound", $"No event log found with ID {id}."));
            return Result.Success(mapper.Map<EventLogDto>(eventLog));
        }, nameof(GetByIdAsync));
    }
}
