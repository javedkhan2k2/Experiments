import { Routes } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';
import { Devices } from './device/devices/devices';
import { Eventlogs } from './eventlog/eventlogs/eventlogs';

export const routes: Routes = [
  {path: 'dashboard', component: Dashboard},
  {path: 'devices', component: Devices},
  {path: 'eventlogs', component: Eventlogs},
  {path: '', redirectTo: 'dashboard', pathMatch: 'full'}
];
