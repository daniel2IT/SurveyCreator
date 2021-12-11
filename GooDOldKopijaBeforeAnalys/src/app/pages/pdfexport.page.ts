import { Component } from "@angular/core";
import * as SurveyPDF from "survey-pdf";
import * as SurveyCore from "survey-core";
import * as widgets from "surveyjs-widgets";

import json from "../../assets/survey.json";
import { DataService } from "app/services/data.service";

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
  selector: "pdfexport-page",
  templateUrl: "./pdfexport.page.html",
})


export class PdfExportPage {

// Behavior
 message:any;

 constructor(private data: DataService){}

 ngOnInit() {
    // Behavior
    this.data.currentMessage.subscribe(message => this.message = message)
 }

  result;
  savePDF() {
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
    


     if(this.message.Code){
        console.log("If for this.message.Code");
       
          const surveyPDF = new SurveyPDF.SurveyPDF(this.message.Code, options);

       surveyPDF.data =this.result;
    surveyPDF.save("WdxSurvey");


    }
    else{
        console.log("Else for this.message.Code " + this.message);

         const surveyPDF = new SurveyPDF.SurveyPDF(JSON.stringify(this.message), options);
        surveyPDF.data = this.result;
    surveyPDF.save("WdxSurvey");

    }

  }
}
