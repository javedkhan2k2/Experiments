import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ClampingDataDto } from '../../_models/clampingDataDto';
import { DatePipe, NgClass } from '@angular/common';
@Component({
  selector: 'app-liveclampingfeed',
  imports: [DatePipe, NgClass],
  templateUrl: './liveclampingfeed.html',
  styleUrl: './liveclampingfeed.css'
})
export class Liveclampingfeed implements OnInit {
  clampingFeed: ClampingDataDto[] = []
  private hubConnection!: signalR.HubConnection;

  ngOnInit(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/hubs/clamping')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().then(() => {
      console.log('SignalR Connected');

      this.hubConnection.on('ReceiveClamping', (data) => {
        this.clampingFeed.unshift(data); // show most recent on top
        if(this.clampingFeed.length > 50) this.clampingFeed.pop(); // keep it light
      });
    });
  }
}
