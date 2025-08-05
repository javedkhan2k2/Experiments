using ClampingDevice.Data;
using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class ClampingDataDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int DeviceId { get; set; }
    [Required]
    public double ClampingForceN { get; set; }
    [Required]
    public double TemperatureC { get; set; }
    public DateTime Timestamp { get; set; }
    public ClampingActionType ActionType { get; set; }
    public bool IsValid { get; set; } = true;
    public string? WarningMessage { get; set; }
    public DeviceDto Device { get; set; } = new DeviceDto();

}
