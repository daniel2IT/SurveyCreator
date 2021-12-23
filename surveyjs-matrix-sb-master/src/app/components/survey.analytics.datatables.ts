import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core";
import * as Survey from "survey-angular";
import { DataTables } from "survey-analytics/survey.analytics.datatables.js";
import { DataService } from "app/services/data.service";
import { SharedService } from "app/services/shared.service";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "survey-analytics-datatables",
  template: ` <div id="surveyAnalyticsContainer"></div> `,
})
export class SurveyAnalyticsDatatablesComponent implements OnInit {
  @Input() json: any;
  @Output() surveySaved: EventEmitter<Object> = new EventEmitter();


  message:any;

  constructor(private data: DataService, private service: SharedService, ) {  
     
  
  }




  ngOnInit() {

    this.data.currentMessage.subscribe(message => this.message = message)

    alert(this.message);




    var json = this.message;

    var survey = new Survey.SurveyModel(json);

    // survey results data object
    var firstResult = {
      organization_type: "In-house",
      developer_count: "1",
      VerticalMarket: "Education",
      product_discovering: "GitHub",
      javascript_frameworks: ["jQuery"],
      backend_language: ["Ruby"],
      useproduct: "Yes",
      uselibraries: ["Survey Library (Runner)"],
      product_new: ["Export to PDF (survey and its result)"],
      supported_devices: ["Desktop", "Tablete", "Mobile"],
      native_mobile_support: "2",
      product_alternative: "Develop ourselves",
      product_recommend: "Yes",
      nps_score: 6,
      product_improvement:
        "The lack of accessibility is a huge disadvantage. That's one reason why I cannot use it in all my projects.",
      native_framework: "",
      survey_cloud_platform: "",
      favorite_functionality: "",
    };

    var secondResult = {
      organization_type: "Consulting",
      developer_count: "3-5",
      VerticalMarket: "Government (federal, state, local)",
      product_discovering: "Search engine",
      javascript_frameworks: ["Vue", "jQuery", "other"],
      backend_language: ["Python", "Node.js"],
      useproduct: "Yes",
      uselibraries: ["Survey Library (Runner)"],
      product_new: [
        "Analytics (Create Analytics based on JSON results)",
        "Export to PDF (survey and its result)",
      ],
      supported_devices: ["Desktop"],
      product_alternative: "Develop ourselves",
      product_recommend: "Yes",
      nps_score: 8,
      "javascript_frameworks-Comment": "AngularJS",
      native_mobile_support: "",
      native_framework: "",
      survey_cloud_platform: "",
      favorite_functionality: "",
      product_improvement: "",
    };

    var data = [firstResult, secondResult];
    // EO survey results data object

    var normalizedData = data.map(function (item) {
      survey.getAllQuestions().forEach(function (q) {
        if (item[q.name] === undefined) {
          item[q.name] = "";
        }
      });
      return item;
    });
    DataTables.initJQuery((<any>window)["jQuery"]);
    var table = new DataTables(<any>survey, normalizedData, null);
    table.render(document.getElementById("surveyAnalyticsContainer"));
  }
}
