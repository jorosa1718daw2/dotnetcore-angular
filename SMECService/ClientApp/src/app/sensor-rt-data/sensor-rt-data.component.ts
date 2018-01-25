import { Component, Inject, Input, HostBinding, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrentAnalogData } from '../models/currentanalogdata.model';
import { Sensor } from '../models/sensor.model';
import { DataService } from '../_services/index'
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { getBaseUrl } from '../../main';

@Component({
  selector: 'sensor-rt-data',
  templateUrl: './sensor-rt-data.component.html'
})
export class SensorRealTimeDataComponent implements OnInit {
  @Input() sensor: Sensor;  
  currentanalogdata: Observable<CurrentAnalogData[]>;
  singleCurrentanalogdata: Observable<CurrentAnalogData>;

  constructor (private dataService: DataService){
   }


  ngOnInit(){
    this.currentanalogdata = this.dataService.currentanalogdata;
    this.singleCurrentanalogdata = this.dataService.currentanalogdata.pipe(
      map(currentanalogdata => currentanalogdata.find(item => item.value === 1))
    );
    this.dataService.loadAll(this.sensor);
    this.dataService.load(1);
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



