import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


import { Focus } from '../models/focus.model';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public focus_list: Focus[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Focus[]>(baseUrl + 'api/Focus').subscribe(result => {
      this.focus_list = result;
    }, error => console.error(error));
  }
}
