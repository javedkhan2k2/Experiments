using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;
using ClampingDevice.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ClampingDevice.Data;

public class EventLogRepository(AppDbContext dbContext, IMapper mapper) : IEventLogRepository
{
    public async Task AddAsync(EventLog eventLog) => await dbContext.EventLogs.AddAsync(eventLog);

    public async Task<PagedList<EventLogDto>> GetAllAsync(EventLogParams eventLogParams)
    {
        var query = dbContext.EventLogs.AsQueryable();
        query = query.OrderByDescending(e => e.Timestamp) // Default ordering by Timestamp
            .AsNoTracking(); // Use AsNoTracking for read-only queries

        return await PagedList<EventLogDto>.CreateAsync(query.ProjectTo<EventLogDto>(mapper.ConfigurationProvider), eventLogParams.PageNumber, eventLogParams.PageSize);
        //return await dbContext.EventLogs
        //    .OrderByDescending(e => e.Timestamp)
        //    .ToListAsync();
    }

    public async Task<EventLog?> GetByIdAsync(int id)
    {
        return await dbContext.EventLogs
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<EventLog>> GetLastFiveAsync()
    {
        return await dbContext.EventLogs
            .OrderByDescending(e => e.Timestamp)
            .Take(5)
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync() => await dbContext.SaveChangesAsync() > 0;
}
