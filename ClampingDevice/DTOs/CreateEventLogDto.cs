using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class CreateEventLogDto
{
    [Required]
    public string EventType { get; init; } = string.Empty;
    [Required]
    public string Message { get; init; } = string.Empty;
}
