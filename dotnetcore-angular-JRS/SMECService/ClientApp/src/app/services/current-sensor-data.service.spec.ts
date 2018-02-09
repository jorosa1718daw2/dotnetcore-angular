import { TestBed, inject } from '@angular/core/testing';

import { CurrentSensorDataService } from './current-sensor-data.service';

describe('CurrentSensorDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CurrentSensorDataService]
    });
  });

  it('should be created', inject([CurrentSensorDataService], (service: CurrentSensorDataService) => {
    expect(service).toBeTruthy();
  }));
});
