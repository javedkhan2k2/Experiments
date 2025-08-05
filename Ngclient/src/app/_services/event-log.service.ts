import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { EventLogDto } from '../_models/eventLogDto';
import { CreateEventLogDto } from '../_models/createEventLogDto';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../helpers/pagination';
import { EventLogParams } from '../helpers/eventLogParams';
import {
  setPaginatedResponse,
  setPaginationHeaders,
} from '../helpers/paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class EventLogService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResult<EventLogDto[]> | null>(null);
  eventLogCache = new Map();
  eventLogParams = signal<EventLogParams>(new EventLogParams());

  resetUserParams() {
    this.eventLogParams.set(new EventLogParams());
  }

  //getAllEventLogs(): Observable<EventLogDto[]>{
  getAllEventLogs() {
    const key = Object.values(this.eventLogParams()).join('-');
    const response = this.eventLogCache.get(key);

    if (response) return setPaginatedResponse(response, this.paginatedResult);

    let params = setPaginationHeaders(
      this.eventLogParams().pageNumber,
      this.eventLogParams().pageSize
    );

    return this.http
      .get<EventLogDto[]>(`${this.baseUrl}eventlog`, {
        observe: 'response',
        params,
      })
      .subscribe({
        next: (response) => {
          setPaginatedResponse(response, this.paginatedResult);
          this.eventLogCache.set(key, response);
        },
      });
  }

  getLastFiveEventLogs() {
    return this.http.get<EventLogDto[]>(`${this.baseUrl}eventlog/last-five`);
  }

  getEventLogById(id: number): Observable<EventLogDto> {
    return this.http.get<EventLogDto>(`${this.baseUrl}eventlog/${id}`);
  }

  createEventLog(dto: CreateEventLogDto): Observable<EventLogDto> {
    return this.http.post<EventLogDto>(`${this.baseUrl}eventlog`, dto);
  }
}
