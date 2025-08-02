using System.ComponentModel.DataAnnotations;

namespace ClampingDevice.Entities;

public class Device
{
    public int Id { get; set; }

    [Required]
    public string SerialNumber { get; set; } = string.Empty;

    [Required]
    public string Model { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public List<ClampingData> ClampingRecords { get; set; } = new();
}
