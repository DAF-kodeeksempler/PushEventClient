export interface DAFEventCollection {
    total: number;
    pagesize: number;
    pagenumber: number;
    dafEvents: DAFEvent[];
}

export interface DAFEvent {
    id: number;
    eventID: number;
    dafEventHistory: DAFEventHistory[];
}

export interface DAFEventHistory {
    id: number;
    time: Date;
    rawBody:string;
    rawFormat:string;
    odataMetadata:string;
    action: number;
    format: number;
    IP:string;

}