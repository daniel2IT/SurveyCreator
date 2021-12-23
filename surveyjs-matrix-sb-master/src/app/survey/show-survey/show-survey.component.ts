import { Component, Inject, OnInit } from '@angular/core';


import * as SurveyPDF from "survey-pdf";
import * as SurveyCore from "survey-core";
import * as widgets from "surveyjs-widgets";


import { SharedService } from 'app/services/shared.service';
import { DataService } from 'app/services/data.service';

import { DOCUMENT } from '@angular/common';

import { Router } from '@angular/router';


widgets.icheck(SurveyCore);
widgets.select2(SurveyCore);
widgets.inputmask(SurveyCore);
widgets.jquerybarrating(SurveyCore);
widgets.jqueryuidatepicker(SurveyCore);
widgets.nouislider(SurveyCore);
widgets.select2tagbox(SurveyCore);
//widgets.signaturepad(SurveyCore);
widgets.sortablejs(SurveyCore);
widgets.ckeditor(SurveyCore);
widgets.autocomplete(SurveyCore);
widgets.bootstrapslider(SurveyCore);
widgets.prettycheckbox(SurveyCore);

@Component({
  selector: 'app-show-survey',
  templateUrl: './show-survey.component.html',
  styleUrls: ['./show-survey.component.css']
})
export class ShowSurveyComponent implements OnInit {

  constructor(private service: SharedService, private data: DataService,
    @Inject(DOCUMENT) private document: Document, private router: Router) { }

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


    this.router.navigateByUrl('/creator');
  }

  // Get Survey Data
  refreshSurveyList(){
    this.service.getSurveyList().subscribe(data=>{
      this.SurveyList = data;
    });
  }


// Export PDF
result;
savePDF(item) {

  this.survey = item;


  var options = {
    fontSize: 14,
    margins: {
      left: 10,
      right: 10,
      top: 5,
      bot: 10,
    },
      format: [210, 297]
  };
  


   if(this.survey.Code){
   
     
        const surveyPDF = new SurveyPDF.SurveyPDF(this.survey.Code, options);

     surveyPDF.data =this.result;
  surveyPDF.save("WdxSurvey");


  }
  else{
    

       const surveyPDF = new SurveyPDF.SurveyPDF(JSON.stringify(this.survey.Code), options);
      surveyPDF.data = this.result;
  surveyPDF.save("WdxSurvey");

  }

}


/// Export Excel
saveExcel(item) {


  this.survey = item;


  // Downlaod Excel Document
  this.document.location.href = 'https://localhost:44341/OverallAnalysis/ExportExcel/' + this.survey.SurveyId;



}

// Redirect To Analysis
goToUrl(item): void {

  this.survey = item;


  this.document.location.href = 'https://localhost:44341/OverallAnalysis/Details/' + this.survey.SurveyId;
}


}
