import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app.routing.module";
import { AppComponent } from "./app.component";
import { HomePage } from "./pages/home.page";
import { SurveyCreatorComponent } from "./components/survey.creator.component";
import { CreatorPage } from "./pages/creator.page";

import { SharedService  } from "./services/shared.service";
import { AddRecipientComponent } from "./recipient/add-recipient/add-recipient.component";
import { ShowSurveyComponent } from "./survey/show-survey/show-survey.component";


import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AnalyticsDatatablesPage } from "./pages/analytics.datatables.page";

import { SurveyAnalyticsDatatablesComponent } from "./components/survey.analytics.datatables";


import { FilterPipe } from "./pipesFilter/filter.pipe";
import { SearchfilterPipe } from "./searchfilter.pipe";


import {ProgressBarModule} from "angular-progress-bar"
import { ShowRecipientComponent } from "./recipient/show-recipient/show-recipient.component";


@NgModule({
  declarations: [
    SurveyAnalyticsDatatablesComponent,
    ShowSurveyComponent,
    AddRecipientComponent,
    AppComponent,
    HomePage,
    SurveyCreatorComponent,
    CreatorPage,
    AnalyticsDatatablesPage,
    SearchfilterPipe,
    ShowRecipientComponent
    
  ],
  imports: [BrowserModule, FormsModule, HttpClientModule, AppRoutingModule, ReactiveFormsModule, ProgressBarModule],
  providers: [ SharedService ],
  bootstrap: [AppComponent],
})
export class AppModule {}
