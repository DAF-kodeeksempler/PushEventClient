import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DAFEvent, DAFEventCollection } from '../api.push-event-client';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html'
})
export class EventList {
  public DAFEvents: DAFEvent[];
  public pagenumber: number = 1;
  public pagesize: number = 100;
  public total: number;
  public loading: boolean;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl
    this.http = http
  }

  ngOnInit() {
    this.getPage(1);
  }

  getPage(pagenumber: number) {
      this.loading = true;
      var zero_indexed_pagenumber = pagenumber - 1
      this.http.get<DAFEventCollection>(this.baseUrl + 'api/DAFevent/list?pagesize=' + this.pagesize + '&pagenumber=' + zero_indexed_pagenumber).subscribe(result => {
        this.DAFEvents = result.dafEvents;
        this.total = result.total;
        this.pagenumber = result.pagenumber + 1; // Do not zero index
        this.loading = false;
      }, error => console.error(error));
  }
}
