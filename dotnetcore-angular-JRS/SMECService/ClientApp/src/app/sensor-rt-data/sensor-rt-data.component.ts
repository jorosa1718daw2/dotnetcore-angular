import { Component, Inject, Input, HostBinding, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrentAnalogData } from '../models/currentanalogdata.model';
import { Sensor } from '../models/sensor.model';
import { CurrentSensorDataService } from '../services/current-sensor-data.service';


@Component({
  selector: 'sensor-rt-data',
  templateUrl: './sensor-rt-data.component.html'
})
export class SensorRealTimeDataComponent implements OnInit {
  @Input() sensor: Sensor;
    
  interval: any;

  public currentAnalogData: CurrentAnalogData;

  constructor (private currentSensorDataService: CurrentSensorDataService) {
    
  }

  refreshData() {
    this.currentSensorDataService.getData(this.sensor.sensorId)
         .subscribe(currentAnalogData => this.currentAnalogData = currentAnalogData);
  }

  ngOnInit(): void {
    this.refreshData();
    this.interval = setInterval(() => {
      this.refreshData();
    }, 500);
  }

  ngOnDestroy() {
    clearInterval(this.interval);
  }
  /*
  public currentAnalogData: CurrentAnalogData;
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit(): void {
    this.http.get<CurrentAnalogData>(this.baseUrl + 'api/Sensor/' + this.sensor.sensorId + '/CurrentAnalogData').subscribe(result => {
      this.currentAnalogData = result;

    }, error => console.error(error));*/
  }



