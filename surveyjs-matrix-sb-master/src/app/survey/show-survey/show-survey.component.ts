import { Component, OnInit } from '@angular/core';
import { SharedService } from 'app/services/shared.service';
import { DataService } from 'app/services/data.service';

@Component({
  selector: 'app-show-survey',
  templateUrl: './show-survey.component.html',
  styleUrls: ['./show-survey.component.css']
})
export class ShowSurveyComponent implements OnInit {

  constructor(private service: SharedService, private data: DataService) { }

  SurveyList: any = [];
  survey: any;

    // Behavior
    message:any;
    ////////////

  ngOnInit() {
    this.refreshSurveyList();
  }

  addClick(item){
    console.log("That works");
    
    this.survey = item;


    this.data.changeMessage(this.survey);

    console.log(this.survey);
    
    console.log(this.survey.Name);
    console.log(this.survey.Code);
    console.log(this.survey.SurveyId);
    
  }

  // Get Survey Data
  refreshSurveyList(){
    this.service.getSurveyList().subscribe(data=>{
      this.SurveyList = data;
    });
  }
}
