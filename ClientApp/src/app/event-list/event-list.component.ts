import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DAFEvent,DAFEventHistory } from '../api.push-event-client';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html'
})
export class EventList {
  public DAFEvents: DAFEvent[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<DAFEvent[]>(baseUrl + 'api/DAFevent/list').subscribe(result => {
      this.DAFEvents = result;
      console.log("Got result!");
    }, error => console.error(error));
  }
}
