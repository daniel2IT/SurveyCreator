import json from '../../assets/survey.json';
import { Component } from '@angular/core';
import {SurveyJS } from '../../assets/SurveyJS.js';


@Component({
  selector: "creator-page",
  templateUrl: "./creator.page.html",
})
export class CreatorPage {

  constructor(){ }

  json = json;
  onSurveySaved(survey) {
    this.json = survey;
  }
}