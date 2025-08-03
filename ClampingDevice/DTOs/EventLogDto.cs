using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class EventLogDto
{
    public int Id { get; init; }
    [Required]
    public string EventType { get; init; } = string.Empty;
    [Required]
    public string Message { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
}