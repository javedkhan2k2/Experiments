namespace ClampingDevice.DTOs;

public class DeviceStatsDto
{
    public int TotalDevices { get; set; }
    public int ActiveDevices { get; set; }
    public int InactiveDevices { get; set; }
    public int UniqueModels { get; set; }
}
