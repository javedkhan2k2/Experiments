using AutoMapper;
using ClampingDevice.Common.Results;
using ClampingDevice.Data;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using ClampingDevice.Helpers;

namespace ClampingDevice.Services;

public class EventLogService(IEventLogRepository eventLogRepository, IMapper mapper, ILogger<EventLogService> logger) : BaseService(logger), IEventLogService
{
    public async Task<Result<EventLogDto>> CreateAsync(CreateEventLogDto dto)
    {
        if (dto is null) return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "CreateEventLogDto cannot be null."));
        if (string.IsNullOrWhiteSpace(dto.EventType)) return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "EventType cannot be null or empty."));
        if (string.IsNullOrWhiteSpace(dto.Message)) return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "Message cannot be null or empty."));

        return await TryExecuteAsync(async () =>
        {
            var eventLog = mapper.Map<EventLog>(dto);
            eventLog.Timestamp = DateTime.UtcNow;
            await eventLogRepository.AddAsync(eventLog);
            if (await eventLogRepository.SaveChangesAsync()) return Result.Success(mapper.Map<EventLogDto>(eventLog));
            return Result.Failure<EventLogDto>(new Error("EventLogCreateError", "Failed to save the event log."));
        }, nameof(CreateAsync));
    }

    public async Task<Result<PagedList<EventLogDto>>> GetAllAsync(EventLogParams eventLogParams)
    {
        return await TryExecuteAsync(async () =>
        {
            var entities = await eventLogRepository.GetAllAsync(eventLogParams);

            if (entities is null || !entities.Any()) return Result.Success(new PagedList<EventLogDto>(Enumerable.Empty<EventLogDto>(), 0, eventLogParams.PageNumber, eventLogParams.PageSize));

            return Result.Success(entities);
        }, nameof(GetAllAsync));
    }

    public async Task<Result<EventLogDto>> GetByIdAsync(int id)
    {
        if (id < 1) return Result.Failure<EventLogDto>(new Error("EventLogGetError", "Id must be greater than zero."));
        return await TryExecuteAsync(async () =>
        {
            var eventLog = await eventLogRepository.GetByIdAsync(id);
            if (eventLog is null) return Result.Failure<EventLogDto>(new Error("NotFound", $"No event log found with ID {id}."));
            return Result.Success(mapper.Map<EventLogDto>(eventLog));
        }, nameof(GetByIdAsync));
    }

    public async Task<Result<IEnumerable<EventLogDto>>> GetLastFiveAsync()
    {
        return await TryExecuteAsync(async () =>
        {
            var entities = await eventLogRepository.GetLastFiveAsync();
            if (entities is null || !entities.Any()) return Result.Success(Enumerable.Empty<EventLogDto>());
            return Result.Success(mapper.Map<IEnumerable<EventLogDto>>(entities));
        }, nameof(GetLastFiveAsync));
    }
}
