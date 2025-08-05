namespace ClampingDevice.DTOs;

public class ClampingStatsDto
{
    public int TotalClampings { get; set; }
    public int TotalUnclampings { get; set; }
    public int FailedClampings { get; set; }
    public DateTime? RecentClampingTimestamp { get; set; }
    public DateTime? RecentUnclampingTimestamp { get; set; }
    public int DevicesInvolved { get; set; }
}
