export interface DeviceDto {
    id: number;
    serialNumber: string;
    model: string;
    location: string;
    isActive: boolean;
    registeredAt: Date;
    lastUpdatedAt: Date | null;
}
