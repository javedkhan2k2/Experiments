using ClampingDevice.Common.Results;
using ClampingDevice.DTOs;

namespace ClampingDevice.Services;

public interface IEventLogService
{
    Task<Result<IEnumerable<EventLogDto>>> GetAllAsync();
    Task<Result<EventLogDto>> GetByIdAsync(int id);
    Task<Result<EventLogDto>> CreateAsync(CreateEventLogDto dto);
}
