export interface ClampingStatsDto {
  totalClampings: number;
  totalUnclampings: number;
  recentClampingTimestamp: Date | null;
  recentUnclampingTimestamp: Date | null;
  failedClampings: number;
  devicesInvolved: number;
}
