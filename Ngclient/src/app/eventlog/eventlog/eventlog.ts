import { Component, inject, OnInit } from '@angular/core';
import { EventLogService } from '../../_services/event-log.service';
import { DatePipe } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { EventLogDto } from '../../_models/eventLogDto';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-eventlog',
  imports: [DatePipe, RouterLink],
  templateUrl: './eventlog.html',
  styleUrl: './eventlog.css',
})
export class Eventlog implements OnInit {
  eventLogService = inject(EventLogService);
  eventLogs: EventLogDto[] | null = null;

  ngOnInit(): void {
    this.loadLastFiveEventLogs();
  }

  loadLastFiveEventLogs() {
    this.eventLogService.getLastFiveEventLogs().subscribe({
      next: response => this.eventLogs = response,
      error: err => console.error(err)
    });
  }

}
