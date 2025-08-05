import { Component, inject, OnInit } from '@angular/core';
import { DeviceService } from '../../_services/device.service';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { DatePipe, NgClass } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-devices',
  imports: [FormsModule, PaginationModule, NgClass, DatePipe, RouterLink],
  templateUrl: './devices.html',
  styleUrl: './devices.css'
})
export class Devices implements OnInit {
  deviceService = inject(DeviceService);

  ngOnInit(): void {
    if(!this.deviceService.paginatedResult()?.items) this.loadDevices();
  }

  loadDevices(){
    this.deviceService.getAllDevices();
  }

  pageChanged(event: any){
    if(this.deviceService.deviceParams().pageNumber !== event.page){
      this.deviceService.deviceParams().pageNumber = event.page;
      this.loadDevices();
    }
  }
}
