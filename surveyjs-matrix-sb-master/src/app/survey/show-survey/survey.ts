type GUID = string & {
    isGuid: true
 };

export interface Survey{
    Name: string;
    Code: string;
    SurveyId: GUID;
}