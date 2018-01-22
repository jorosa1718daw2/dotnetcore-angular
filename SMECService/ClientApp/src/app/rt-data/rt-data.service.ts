import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RealTimeDataService {


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {



  }

}
