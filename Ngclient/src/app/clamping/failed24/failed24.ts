import { Component, inject, OnInit } from '@angular/core';
import { ClampingService } from '../../_services/clamping.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-failed24',
  imports: [NgClass],
  templateUrl: './failed24.html',
  styleUrl: './failed24.css'
})
export class Failed24 implements OnInit {
  clampingService = inject(ClampingService);
  failedClampings: number = 0;

  ngOnInit(): void {
    this.loadFailedClampingData();
  }

  loadFailedClampingData() {
    this.clampingService.getFailedClampings().subscribe({
      next: response => this.failedClampings = response,
      error: err => console.error(err)
    })
  }

}
