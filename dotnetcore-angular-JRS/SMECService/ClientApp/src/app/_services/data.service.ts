import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
import { Injectable, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Focus } from '../models/focus.model'
import { CurrentAnalogData } from '../models/currentanalogdata.model';
import { Sensor } from '../models/sensor.model';

@Injectable()
export class DataService {
    currentanalogdata: Observable<CurrentAnalogData[]>;
    private _currentanalogdata: BehaviorSubject<CurrentAnalogData[]>;
    private dataStore: { currentanalogdata: CurrentAnalogData[] };
    sensor : Sensor[];

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.dataStore = { currentanalogdata: [] };
        this._currentanalogdata = <BehaviorSubject<CurrentAnalogData[]>>new BehaviorSubject([]);
        this.currentanalogdata = this._currentanalogdata.asObservable();
    }

    /*
this.http.get<CurrentAnalogData>(this.baseUrl + 'api/Sensor/' + this.sensor.sensorId + '/CurrentAnalogData').subscribe(result => {
this.currentAnalogData = result;

    }, error => console.error(error));
  }

  
}*/

    loadAll(sensor) {
        this.http.get(`http://192.168.10.105/api/Sensor/${sensor.sensorId}/CurrentAnalogData`).subscribe((data: CurrentAnalogData[]) => {
            this.dataStore.currentanalogdata = data;
            this._currentanalogdata.next(Object.assign({}, this.dataStore).currentanalogdata);
        }, error => console.log('No se han Podido cargar loadAll()'));
    }

    load(sensor) {
        this.http.get<CurrentAnalogData>(`http://192.168.10.105/api/Sensor/${sensor.sensorId}/CurrentAnalogData`).subscribe((data: CurrentAnalogData) => {
            let notFound = true;

            this.dataStore.currentanalogdata.forEach((item, index) => {
                if (item.value === data.value) {
                    this.dataStore.currentanalogdata[index] = data;
                    notFound = false;
                }
            });

            if (notFound) {
                this.dataStore.currentanalogdata.push(data);
            }

            this._currentanalogdata.next(Object.assign({}, this.dataStore).currentanalogdata);
        }, error => console.log('No se Ha podido cargar load()'));
    }
    create(currentanalogdata: CurrentAnalogData, sensor){
        this.http.post(`http://192.168.10.105/api/Sensor/${sensor.sensorId}/CurrentAnalogData`, JSON.stringify(currentanalogdata))
            .subscribe((data:CurrentAnalogData) => {
                this.dataStore.currentanalogdata.push(data);
                this._currentanalogdata.next(Object.assign({}, this.dataStore).currentanalogdata);
            }, error => console.log('No se ha podido crear'));
    }
    update(currentanalogdata: CurrentAnalogData, sensor) {
        this.http.put(`http://192.168.10.105/api/Sensor/${sensor.sensorId}/CurrentAnalogData`, JSON.stringify(currentanalogdata))
            .subscribe((data: CurrentAnalogData) => {
                this.dataStore.currentanalogdata.forEach((t, i) => {
                    if (t.value === data.value) { this.dataStore.currentanalogdata[i] = data; }
                });
                this._currentanalogdata.next(Object.assign({}, this.dataStore).currentanalogdata);
            }, error => console.log('No se ha podido actualizar'));

    }
    remove(currentanalogdata: CurrentAnalogData, sensor){
        this.http.delete(`http://192.168.10.105/api/Sensor/${sensor.sensorId}/CurrentAnalogData`)
            .subscribe(response =>{
                this.dataStore.currentanalogdata.forEach((t, i) => { 
                    if (t.value === currentanalogdata.value) { this.dataStore.currentanalogdata.splice(i, 1); }
                });
                this._currentanalogdata.next(Object.assign({}, this.dataStore).currentanalogdata);
            }, error => console.log('No se ha podido eliminar'));
    }
}