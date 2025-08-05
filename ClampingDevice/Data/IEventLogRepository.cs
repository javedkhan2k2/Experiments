using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using ClampingDevice.Helpers;

namespace ClampingDevice.Data;

public interface IEventLogRepository
{
    Task<PagedList<EventLogDto>> GetAllAsync(EventLogParams eventLogParams);
    Task<EventLog?> GetByIdAsync(int id);
    Task AddAsync(EventLog eventLog);
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<EventLog>> GetLastFiveAsync();
}
