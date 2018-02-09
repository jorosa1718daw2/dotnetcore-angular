import { Sensor } from './sensor.model';


export interface Analyzer {
  manufacturer: number;
  model: string;
  serialNumber: string;
  sensors: Sensor[];
}
