import { Component, inject, OnInit } from '@angular/core';
import { EventLogService } from '../../_services/event-log.service';
import { DatePipe } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-eventlogs',
  imports: [DatePipe, PaginationModule, FormsModule, RouterLink],
  templateUrl: './eventlogs.html',
  styleUrl: './eventlogs.css'
})
export class Eventlogs implements OnInit {
  eventLogService = inject(EventLogService);

  ngOnInit(): void {
    if(!this.eventLogService.paginatedResult()?.items) this.loadEventLogs();
  }

  loadEventLogs(){
    this.eventLogService.getAllEventLogs();
  }

  pageChanged(event: any) {
    if (this.eventLogService.eventLogParams().pageNumber !== event.page) {
      this.eventLogService.eventLogParams().pageNumber = event.page;
      this.loadEventLogs();
    }
  }

}
