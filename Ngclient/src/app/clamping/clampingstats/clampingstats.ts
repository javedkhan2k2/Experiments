import { Component, inject, OnInit } from '@angular/core';
import { ClampingService } from '../../_services/clamping.service';
import { ClampingStatsDto } from '../../_models/clampingStatsDto';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-clampingstats',
  imports: [DatePipe],
  templateUrl: './clampingstats.html',
  styleUrl: './clampingstats.css'
})
export class Clampingstats implements OnInit {
  clampingService = inject(ClampingService);
  clampingStats: ClampingStatsDto | null = null;

  ngOnInit(): void {
    this.loadClampingStats();
  }

  loadClampingStats(){
    this.clampingService.getClampingStats().subscribe({
      next: response => this.clampingStats = response,
      error: err => console.error(err)
    })
  }

}
