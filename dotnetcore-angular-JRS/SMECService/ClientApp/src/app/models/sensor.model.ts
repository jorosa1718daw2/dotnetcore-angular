import { MeasuringComponent } from './measuringcomponent.model';
import { Unit } from './unit.model';

export interface Sensor {
  sensorId: number;
  measuringComponent: MeasuringComponent;
  unit: Unit;
}
