using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.Entities;

public class ClampingData
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int DeviceId { get; set; }
    [Required]
    [Range(500, 1500)]
    public double ClampingForceN { get; set; }
    [Required]
    [Range(10, 60)]
    public double TemperatureC { get; set; }
    [Required]
    public DateTime Timestamp { get; set; }
    public Device Device { get; set; } = null!; // Navigation property for the related Device entity
    public bool IsValid { get; set; } = true;
    public string? WarningMessage { get; set; }
}
