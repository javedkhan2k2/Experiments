import { Component, inject, OnInit } from '@angular/core';
import { Eventlog } from "../eventlog/eventlog/eventlog";
import { Devicestats } from "../device/devicestats/devicestats";
import { Clampingstats } from "../clamping/clampingstats/clampingstats";
import { DeviceTable } from "../device/device-table/device-table";
import { SystemStatusCard } from "../system-status-card/system-status-card";
import { Navigationcard } from "../navigationcard/navigationcard";
import { Failed24 } from "../clamping/failed24/failed24";
import { Liveclampingfeed } from "../clamping/liveclampingfeed/liveclampingfeed";

@Component({
  selector: 'app-dashboard',
  imports: [Eventlog, Devicestats, Clampingstats, DeviceTable, SystemStatusCard, Navigationcard, Failed24, Liveclampingfeed],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

}
