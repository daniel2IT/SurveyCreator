import * as SurveyCreator from 'survey-creator';
import * as SurveyKo from 'survey-knockout';
import * as widgets from 'surveyjs-widgets';
import { init as initCustomWidget } from './customwidget';
import { Component, EventEmitter, Input, Output, HostListener } from '@angular/core';
import { DataService } from 'app/services/data.service';

widgets.icheck(SurveyKo);
widgets.select2(SurveyKo);
widgets.inputmask(SurveyKo);
widgets.jquerybarrating(SurveyKo);
widgets.jqueryuidatepicker(SurveyKo);
widgets.nouislider(SurveyKo);
widgets.select2tagbox(SurveyKo);
//widgets.signaturepad(SurveyKo);
widgets.sortablejs(SurveyKo);
widgets.ckeditor(SurveyKo);
widgets.autocomplete(SurveyKo);
widgets.bootstrapslider(SurveyKo);
//widgets.emotionsratings(SurveyKo);
initCustomWidget(SurveyKo);

SurveyCreator.StylesManager.applyTheme("modern");

// var CkEditor_ModalEditor = {
//   afterRender: function(modalEditor, htmlElement) {
//     var editor = window["CKEDITOR"].replace(htmlElement);
//     editor.on("change", function() {
//       modalEditor.editingValue = editor.getData();
//     });
//     editor.setData(modalEditor.editingValue);
//   },
//   destroy: function(modalEditor, htmlElement) {
//     var instance = window["CKEDITOR"].instances[htmlElement.id];
//     if (instance) {
//       instance.removeAllListeners();
//       window["CKEDITOR"].remove(instance);
//     }
//   }
// };
// SurveyCreator.SurveyPropertyModalEditor.registerCustomWidget(
//   "html",
//   CkEditor_ModalEditor
// );
// CKEditor is the third party library
/* Steps to use CKEditor in the project:
    1. run 'npm i ckeditor'
    2. update the "build" section of the angular.json: add ckeditor.js script in the scripts section.
*/


@Component({
  selector: "survey-creator",
  template: `
    <div id="surveyCreatorContainer"></div>
  `
})




export class SurveyCreatorComponent {


  surveyCreator: SurveyCreator.SurveyCreator;
  @Input() json: any;
  @Output() surveySaved: EventEmitter<Object> = new EventEmitter();

 // Behavior
 message:any;
 ////////////

 /*
   if(this.message){
       // Behavior
      this.surveyCreator.text = this.message.Code;
         this.surveyCreator.saveSurveyFunc = this.message.Code;
    }
    else{
        console.log("Else Works ");
         this.surveyCreator.text = JSON.stringify(this.json);
          this.surveyCreator.saveSurveyFunc = this.saveMySurvey;

           this.data.changeMessage(JSON.stringify(this.json));
    }*/


 constructor(private data: DataService){}

 ngOnInit() {
  
   // Behavior
   this.data.currentMessage.subscribe(message => this.message = message)

    SurveyKo.JsonObject.metaData.addProperty(
      "questionbase",
      "popupdescription:text"
    );
    SurveyKo.JsonObject.metaData.addProperty("page", "popupdescription:text");

    let options = {
       showEmbededSurveyTab: true, 
      showTranslationTab: true
     };
    this.surveyCreator = new SurveyCreator.SurveyCreator(
      "surveyCreatorContainer",
      options
    );

    // Main 
    this.surveyCreator.toolbox.removeItem("image");
    this.surveyCreator.toolbox.removeItem("matrixdropdown");
    this.surveyCreator.toolbox.removeItem("matrix");
    this.surveyCreator.toolbox.removeItem("paneldynamic");
    this.surveyCreator.toolbox.removeItem("html");
    this.surveyCreator.toolbox.removeItem("expression");
    this.surveyCreator.toolbox.removeItem("textwithbutton");
    this.surveyCreator.toolbox.removeItem("bootstrapslider");
   // this.surveyCreator.toolbox.removeItem("matrixdynamic");


  if(this.message.Code){

      this.surveyCreator.text = this.message.Code;

      // Set Title
      var setTitle =  JSON.parse(this.surveyCreator.text);
      Object.assign(setTitle["pages"][0], {title: this.message.Name });
      var justTest = JSON.stringify(setTitle);
      this.surveyCreator.text = justTest;

    }
    else if(this.message){
        console.log("Else for this.message.Code " + this.message);

       this.surveyCreator.text = JSON.stringify(this.message);

    }
    else{
      //alert("This one working good");
     //this.surveyCreator.text = JSON.stringify(this.json); 
    }

 this.surveyCreator.saveSurveyFunc = this.saveMySurvey;
 
  }

  saveMySurvey = () => {
    this.surveySaved.emit(JSON.parse(this.surveyCreator.text));
    this.data.changeMessage(JSON.parse(this.surveyCreator.text));
    console.log(JSON.stringify(this.message));
  };
}
