using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class ClampingDataDto
{
    [Required]
    public int DeviceId { get; set; }
    [Required]
    public double ClampingForceN { get; set; }
    [Required]
    public double TemperatureC { get; set; }
    public DateTime Timestamp { get; set; }
}
