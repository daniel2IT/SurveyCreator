import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})


export class SharedService {

  readonly APIUrl = "https://localhost:44341/api";
  readonly MVCUrl = "https://localhost:44341"

  constructor(private http:HttpClient) {}
    
    getSurveyList():Observable<any[]>
    {
      return this.http.get<any>(this.APIUrl + '/survey');
    }

    getSurveyResulList():Observable<any[]>
    {
      return this.http.get<any>(this.APIUrl + '/results');
    }



    getRecipientList():Observable<any[]>
    {
      return this.http.get<any>(this.APIUrl + '/recipient');
    }

    addRecipient(val:any[])
    {
      return this.http.post(this.APIUrl+'/recipient',val);
    }

    getRecipientResult(valId:any){
      return this.http.get(this.APIUrl + '/recipient', valId);
    }

    deleteRecipient(valId:any)
    {
      return this.http.delete(this.APIUrl + '/recipient', valId);
    }
  
}