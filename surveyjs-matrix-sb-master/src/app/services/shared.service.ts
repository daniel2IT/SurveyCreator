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

    addSurvey(val:any)
    {
      return this.http.post(this.APIUrl+'/survey',val);
    }

    addRecipient(val:any)
    {
      return this.http.post(this.APIUrl+'/recipient',val);
    }
  
    updateSurvey(val:any)
    {
      return this.http.put(this.APIUrl+'/survey',val);
    }

  
    //deleteExpense(valNo:any)
    //{
    //  return this.http.delete(this.APIUrl+'/expense/'+ valNo);
    //}
}