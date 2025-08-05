import { Component, inject, OnInit } from '@angular/core';
import { DeviceService } from '../../_services/device.service';
import { DeviceStatsDto } from '../../_models/deviceStatsDto';

@Component({
  selector: 'app-devicestats',
  imports: [],
  templateUrl: './devicestats.html',
  styleUrl: './devicestats.css'
})
export class Devicestats implements OnInit {
  deviceService = inject(DeviceService);
  deviceStats: DeviceStatsDto | null = null;

  ngOnInit(): void {
    //if(!this.deviceService.deviceStats()) this.loadDeviceStats();
    this.loadDeviceStats();
  }

  loadDeviceStats(){
    this.deviceService.getDeviceStats().subscribe({
      next: response => this.deviceStats = response,
      error: err => console.error(err)
    });
  }

}
