import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-system-status-card',
  imports: [DatePipe],
  templateUrl: './system-status-card.html',
  styleUrl: './system-status-card.css'
})
export class SystemStatusCard {
  lastSyncTime = Date.now();
  failedClampingsCount = 3;
}
