export interface DAFEvent {
    id: number;
    eventID: number;
    dafEventHistory: DAFEventHistory[]; 
}

export interface DAFEventHistory {
    id: number;
    action: number;
    time: Date;
    rawFormat:string;
    rawBody:string; 
}