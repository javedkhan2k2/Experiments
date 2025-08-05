import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { DeviceDto } from '../_models/deviceDto';
import { Observable } from 'rxjs';
import { CreateDeviceDto } from '../_models/createDeviceDto';
import { UpdateDeviceDto } from '../_models/updateDeviceDto';
import { DeviceStatusDto } from '../_models/deviceStatusDto';
import { DeviceStatsDto } from '../_models/deviceStatsDto';
import { PaginatedResult } from '../helpers/pagination';
import { DeviceParams } from '../helpers/deviceParams';
import { setPaginatedResponse, setPaginationHeaders } from '../helpers/paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<DeviceDto[]> | null>(null)
  deviceCache = new Map();
  deviceParams = signal<DeviceParams>( new DeviceParams());

  resetUserParams() {
      this.deviceParams.set(new DeviceParams());
    }

  getAllDevices() {
    const key = Object.values(this.deviceParams()).join('-');
    const response = this.deviceCache.get(key);
    if(response){
      setPaginatedResponse(response, this.paginatedResult);
      return;
    }

    let params = setPaginationHeaders(this.deviceParams().pageNumber, this.deviceParams().pageSize);

    return this.http.get<DeviceDto[]>(`${this.baseUrl}device`, {observe: 'response', params}).subscribe({
      next: response => {
        setPaginatedResponse(response, this.paginatedResult);
        this.deviceCache.set(key, response);
      },
      error: err => console.error(err)
    });
  }

  getLastFiveDevices() {
    return this.http.get<DeviceDto[]>(`${this.baseUrl}device/last-five`);
  }

  getDeviceStats() {
    return this.http
      .get<DeviceStatsDto>(`${this.baseUrl}device/stats`);
  }

  getDeviceBySerialNumber(serialNumber: string): Observable<DeviceDto> {
    return this.http.get<DeviceDto>(`${this.baseUrl}device/${serialNumber}`);
  }

  registerDevice(dto: CreateDeviceDto): Observable<DeviceDto> {
    return this.http.post<DeviceDto>(`${this.baseUrl}device`, dto);
  }

  updateDevice(serialNumber: string, dto: UpdateDeviceDto) {
    return this.http.put(`${this.baseUrl}device/${serialNumber}`, dto);
  }

  // This function only soft delete the device
  deleteDevice(serialNumber: string) {
    return this.http.delete(`${this.baseUrl}device/${serialNumber}`);
  }

  toggleDeviceActive(serialNumber: string) {
    return this.http.patch(
      `${this.baseUrl}device/${serialNumber}/toggle-active`,
      {}
    );
  }

  getDeviceStatus(serialNumber: string): Observable<DeviceStatusDto> {
    return this.http.get<DeviceStatusDto>(
      `${this.baseUrl}device/${serialNumber}/status`
    );
  }
}
