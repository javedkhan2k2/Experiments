using ClampingDevice.Entities;

namespace ClampingDevice.Data;

public interface IEventLogRepository
{
    Task<IEnumerable<EventLog>> GetAllAsync();
    Task<EventLog?> GetByIdAsync(int id);
    Task AddAsync(EventLog eventLog);
    Task<bool> SaveChangesAsync();
}
