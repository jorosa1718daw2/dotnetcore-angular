import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Focus } from '../models/focus.model';
import { FocusService } from '../services/focus.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public focus_list: Focus[];

  constructor(private focusService: FocusService) {
  }

  refreshData() {
    this.focusService.getData()
      .subscribe(focus_list => this.focus_list = focus_list);
  }

  ngOnInit(): void {
    this.refreshData()
  }
}
