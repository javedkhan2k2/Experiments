import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { DeviceDto } from '../_models/deviceDto';
import { Observable } from 'rxjs';
import { CreateDeviceDto } from '../_models/createDeviceDto';
import { UpdateDeviceDto } from '../_models/updateDeviceDto';
import { DeviceStatusDto } from '../_models/deviceStatusDto';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAllDevices(): Observable<DeviceDto[]>{
    return this.http.get<DeviceDto[]>(`${this.baseUrl}device`);
  }

  getDeviceBySerialNumber(serialNumber: string): Observable<DeviceDto>{
    return this.http.get<DeviceDto>(`${this.baseUrl}device/${serialNumber}`);
  }

  registerDevice(dto: CreateDeviceDto): Observable<DeviceDto>{
    return this.http.post<DeviceDto>(`${this.baseUrl}device`, dto);
  }

  updateDevice(serialNumber: string, dto: UpdateDeviceDto) {
    return this.http.put(`${this.baseUrl}device/${serialNumber}`, dto);
  }

  // This function only soft delete the device
  deleteDevice(serialNumber: string){
    return this.http.delete(`${this.baseUrl}device/${serialNumber}`);
  }

  toggleDeviceActive(serialNumber: string) {
    return this.http.patch(`${this.baseUrl}device/${serialNumber}/toggle-active`, {});
  }

  getDeviceStatus(serialNumber: string): Observable<DeviceStatusDto> {
    return this.http.get<DeviceStatusDto>(`${this.baseUrl}device/${serialNumber}/status`);
  }
}
