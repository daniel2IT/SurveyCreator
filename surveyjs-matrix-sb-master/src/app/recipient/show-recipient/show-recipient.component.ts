import { Component, Inject, OnInit } from '@angular/core';


import * as SurveyPDF from "survey-pdf";
import * as SurveyCore from "survey-core";
import * as widgets from "surveyjs-widgets";


import { SharedService } from 'app/services/shared.service';
import { DataService } from 'app/services/data.service';

import { DOCUMENT } from '@angular/common';

import { Router } from '@angular/router';
import { Survey } from '../../survey/show-survey/survey';



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
  selector: 'app-show-recipient',
  templateUrl: './show-recipient.component.html',
  styleUrls: ['./show-recipient.component.css']
})
export class ShowRecipientComponent implements OnInit {


  searchValue = '';

  constructor(private service: SharedService, private data: DataService,
    @Inject(DOCUMENT) private document: Document, private router: Router) { }
  transform(value: any, ...args: any[]) {
    throw new Error('Method not implemented.');
  }

  RecipientList: Survey[];
  recipient: any;

    // Behavior
    message:any;
    ////////////

  ngOnInit() {
    this.refreshRecipientList();
  }



  // Get Survey Data
  refreshRecipientList(){
    this.service.getRecipientList().subscribe(data=>{
      this.RecipientList = data;
    });
  }



deleteClick(item: any)
{
  if(confirm("Are you sure?"))
  {
    this.service.deleteRecipient(item.RecipientId).subscribe(data =>
    {
      alert(data.toString());
      this.refreshRecipientList();
    })
  }
}

// Redirect To Analysis
goToUrl(item): void {

  this.recipient = item;


  this.document.location.href = 'https://localhost:44341/Analyser/Details/' + this.recipient.CompletedId;
}
}
