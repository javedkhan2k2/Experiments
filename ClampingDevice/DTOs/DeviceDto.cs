namespace ClampingDevice.DTOs;

public class DeviceDto
{
    public int Id { get; set; }

    public string SerialNumber { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public DateTime RegisteredAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

}
