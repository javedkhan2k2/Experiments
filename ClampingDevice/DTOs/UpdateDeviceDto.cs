using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.DTOs;

public class UpdateDeviceDto
{
    [Required]
    public string Model { get; set; }
    [Required]
    public string Location { get; set; }
    [Required]
    public bool IsActive { get; set; }
}
