import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';

@Injectable({
  providedIn: 'root'
})
export class DataService {

 // Behavior variables
 private messageSource = new BehaviorSubject<any>("");
 currentMessage = this.messageSource.asObservable();

  constructor() { }

// Behavior Function Changes current value
changeMessage(message: any){
  this.messageSource.next(message)
  }
}
