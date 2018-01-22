import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public focus_list: Focus[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Focus[]>(baseUrl + 'api/Focus').subscribe(result => {
      this.focus_list = result;
      for (let focus of this.focus_list)
        for (let analyzer of focus.analyzers)
          for (let sensor of analyzer.sensors) {
            http.get<CurrentAnalogData>(baseUrl + 'api/Sensor/' + sensor.sensorId +  '/CurrentAnalogData').subscribe(result => {
              sensor.currentAnalogData = result;
            }, error => console.error(error));
      }
    }, error => console.error(error));
  }
}

interface CurrentAnalogData {
  value: number;
  statusCode: number;
  samples: number;
}

interface Unit {
  name: string;
}

interface MeasuringComponent {
  name: string;
}

interface Sensor {
  sensorId: number;
  measuringComponent: MeasuringComponent;
  unit: Unit;
  currentAnalogData: CurrentAnalogData;
}

interface Analyzer {
  manufacturer: number;
  model: string;
  serialNumber: string;
  sensors: Sensor[];
}


interface Focus {
  focusId: number;
  name: string;
  description: string;
  analyzers: Analyzer[];
}

