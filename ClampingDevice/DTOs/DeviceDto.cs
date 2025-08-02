using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class DeviceDto
{
    [Required]
    public string SerialNumber { get; set; } = string.Empty;

    [Required]
    public string Model { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;
}
