namespace ClampingDevice.DTOs;

public class DeviceStatusDto
{
    public string SerialNumber { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
