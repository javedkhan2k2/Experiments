import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { EventLogDto } from '../_models/eventLogDto';
import { CreateEventLogDto } from '../_models/createEventLogDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EventLogService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAllEventLogs(): Observable<EventLogDto[]>{
    return this.http.get<EventLogDto[]>(`${this.baseUrl}eventlog`);
  }

  getEventLogById(id: number): Observable<EventLogDto>{
    return this.http.get<EventLogDto>(`${this.baseUrl}eventlog/${id}`);
  }

  createEventLog(dto: CreateEventLogDto): Observable<EventLogDto>{
    return this.http.post<EventLogDto>(`${this.baseUrl}eventlog`, dto);
  }
}
