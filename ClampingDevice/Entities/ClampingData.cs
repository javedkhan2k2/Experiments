using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.Entities;

public class ClampingData
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int DeviceId { get; set; }
    [Required]
    public double ClampingForceN { get; set; }
    [Required]
    public double TemperatureC { get; set; }
    [Required]
    public DateTime Timestamp { get; set; }
    public bool IsValid { get; set; } = true;
    public string? WarningMessage { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Navigation property for the related Device entity
    public Device Device { get; set; } = null!;
}
