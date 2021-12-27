import { Pipe, PipeTransform } from '@angular/core';
import { Survey } from './survey/show-survey/survey';

@Pipe({
  name: 'searchfilter'
})
export class SearchfilterPipe implements PipeTransform {

  transform(Surveys: Survey[], searchValue: string): Survey[] {

    if(!Surveys || !searchValue){
      return Surveys;
    }
    
    return Surveys.filter(survey => 
      survey.Name.toLocaleLowerCase().includes(searchValue.toLocaleLowerCase()));
  }

}
