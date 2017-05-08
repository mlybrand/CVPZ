import { SystemStatusService } from './../system-status.service';
import { HttpModule } from '@angular/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-system-status-component',
  templateUrl: './system-status-component.component.html',
  styleUrls: ['./system-status-component.component.css']
})
export class SystemStatusComponentComponent implements OnInit {

  profileServiceStatus: boolean = false;

  constructor(private systemStatusService: SystemStatusService) { }

  ngOnInit() { }

  doStatusCheck() {
    this.systemStatusService.getStatusProfileService().subscribe(x => this.profileServiceStatus = x);
  }
}
