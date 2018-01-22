import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-focus',
  templateUrl: './focus.component.html'
})
export class FocusComponent {
  public focus_list: Focus[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Focus[]>(baseUrl + 'api/Focus').subscribe(result => {
      this.focus_list = result;
    }, error => console.error(error));
  }
}

interface Analyzer {
  manufacturer: number;
  model: string;
  serialNumber: string;
}


interface Focus {
  focusId: number;
  name: string;
  description: string;
  analyzers: Analyzer[];
}



