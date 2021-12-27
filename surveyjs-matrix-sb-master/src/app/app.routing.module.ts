import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomePage } from "./pages/home.page";
import { CreatorPage } from "./pages/creator.page";


import { AddRecipientComponent } from "./recipient/add-recipient/add-recipient.component";
import { ShowSurveyComponent } from "./survey/show-survey/show-survey.component";
import { AnalyticsDatatablesPage } from "./pages/analytics.datatables.page";
import { SurveyAnalyticsDatatablesComponent } from "./components/survey.analytics.datatables";
import { PdfExportPage } from "./pages/pdfexport.page";
import { ShowRecipientComponent } from "./recipient/show-recipient/show-recipient.component";




const routes: Routes = [
  { path: "", component: HomePage },
  { path: "creator", component: CreatorPage },
  { path: "recipient", component: AddRecipientComponent},
  { path: "surveys", component: ShowSurveyComponent},
  { path: "excel", component: SurveyAnalyticsDatatablesComponent},
  { path: "recipients", component: ShowRecipientComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
