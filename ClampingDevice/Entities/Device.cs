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

    [Required]
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }

    public List<ClampingData> ClampingRecords { get; set; } = new();
}
