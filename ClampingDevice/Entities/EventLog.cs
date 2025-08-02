using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.Entities;

public class EventLog
{
    public int Id { get; set; }
    [Required]
    public string EventType { get; set; } = string.Empty; // e.g., "ClampDataReceived", "Error", "Alert"
    [Required]
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
