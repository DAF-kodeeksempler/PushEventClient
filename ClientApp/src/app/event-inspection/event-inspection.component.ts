import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DAFEvent,DAFEventHistory } from '../api.push-event-client';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-event-inspection',
  templateUrl: './event-inspection.component.html'
})
export class EventInspection implements OnInit {
  public DAFEvent: DAFEvent;
  public id: Number;
  private http : HttpClient;
  private baseUrl : string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.http = http;
    this.baseUrl = baseUrl
  }
  ngOnInit() {
    // Get the param
    const routeParams = this.route.snapshot.paramMap;
    this.id = Number(routeParams.get('id'));
    
    // Call the API
    this.http.get<DAFEvent>(this.baseUrl + 'api/DAFevent/' + this.id).subscribe(result => {
      
      this.DAFEvent = result;
      
      // Ugly, but remap json string into actual data
      this.DAFEvent.dafEventHistory = this.DAFEvent.dafEventHistory.map(
        (e) => {
          if (e.rawFormat === "JSON"){
            e.rawBody = JSON.parse(e.rawBody)
          }
          return e
        }
      ) 
      console.log(result);
    }, error => console.error(error)); 
  }
}
