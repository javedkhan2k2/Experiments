import { Component, inject, OnInit } from '@angular/core';
import { DeviceService } from '../../_services/device.service';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgClass } from '@angular/common';
import { DeviceDto } from '../../_models/deviceDto';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-device-table',
  imports: [PaginationModule, FormsModule, DatePipe, NgClass, RouterLink],
  templateUrl: './device-table.html',
  styleUrl: './device-table.css'
})
export class DeviceTable implements OnInit {
  deviceService = inject(DeviceService);
  devices: DeviceDto[] | null = null;

  ngOnInit(): void {
    this.loadLastFiveDevices();
  }

  loadLastFiveDevices() {
    this.deviceService.getLastFiveDevices().subscribe({
      next: response => this.devices = response,
      error: err => console.error(err)
    });
  }


}
