import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Focus } from '../models/focus.model';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class FocusService {
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getData(): Observable<Focus[]> {
    return this.http.get<Focus[]>(this.baseUrl + 'api/Focus')
  }
}
