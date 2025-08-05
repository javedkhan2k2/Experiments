using ClampingDevice.Data;
using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class CreateClampingDataDto
{
    [Required]
    public string serialNumber { get; set; } = string.Empty;
    [Required]
    public double ClampingForceN { get; set; }
    [Required]
    public double TemperatureC { get; set; }
    public ClampingActionType ActionType { get; set; }

}
