using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;
using ClampingDevice.Helpers;

namespace ClampingDevice.Services;

public interface IEventLogService
{
    Task<Result<PagedList<EventLogDto>>> GetAllAsync(EventLogParams eventLogParams);
    Task<Result<EventLogDto>> GetByIdAsync(int id);
    Task<Result<EventLogDto>> CreateAsync(CreateEventLogDto dto);
    Task<Result<IEnumerable<EventLogDto>>> GetLastFiveAsync();
}
