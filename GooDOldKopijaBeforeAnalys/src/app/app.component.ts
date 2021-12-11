import { Component } from "@angular/core";
import { ShowSurveyComponent } from "./survey/show-survey/show-survey.component";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
  providers:[ShowSurveyComponent]
})
export class AppComponent {
  title = "WDX Survey Creator";
}
