using ClampingDevice.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

public class EventLogRepository(AppDbContext dbContext) : IEventLogRepository
{
    public async Task AddAsync(EventLog eventLog) => await dbContext.EventLogs.AddAsync(eventLog);

    public async Task<IEnumerable<EventLog>> GetAllAsync()
    {
        return await dbContext.EventLogs
            .OrderByDescending(e => e.Timestamp)
            .ToListAsync();
    }

    public async Task<EventLog?> GetByIdAsync(int id)
    {
        return await dbContext.EventLogs
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> SaveChangesAsync() => await dbContext.SaveChangesAsync() > 0;
}
