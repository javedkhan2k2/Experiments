import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UpdateClampingDataDto } from '../_models/updateClampingDataDto';
import { ClampingDataDto } from '../_models/clampingDataDto';
import { Observable } from 'rxjs';
import { CreateClampingDataDto } from '../_models/createClampingDataDto';
import { ClampingStatsDto } from '../_models/clampingStatsDto';

@Injectable({
  providedIn: 'root'
})
export class ClampingService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAllClampingData(): Observable<ClampingDataDto[]> {
    return this.http.get<ClampingDataDto[]>(`${this.baseUrl}clamping`);
  }

  getClampingStats() {
    return this.http.get<ClampingStatsDto>(`${this.baseUrl}clamping/stats`)
  }

  getFailedClampings() {
    return this.http.get<number>(`${this.baseUrl}clamping/failed`)
  }

  getClampingDataById(id: number): Observable<ClampingDataDto>{
    return this.http.get<ClampingDataDto>(`${this.baseUrl}clamping/${id}`);
  }

  createClampingData(dto: CreateClampingDataDto): Observable<ClampingDataDto> {
    return this.http.post<ClampingDataDto>(`${this.baseUrl}clamping`, dto);
  }

  updateClampingData(id: number, dto: UpdateClampingDataDto)
  {

  }

  deleteClampingData(id: number){
    return this.http.delete(`${this.baseUrl}clamping/${id}`);
  }

}
