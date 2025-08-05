import { DeviceDto } from "./deviceDto";

export interface ClampingDataDto {
    id: number;
    deviceId: number;
    clampingForceN: number;
    temperatureC: number;
    timestamp: Date;
    actionType: number;
    isValid: boolean;
    warningMessage: string | null;
    device: DeviceDto;
}
