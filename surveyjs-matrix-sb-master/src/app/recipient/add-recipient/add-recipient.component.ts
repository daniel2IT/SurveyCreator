import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, FormBuilder } from '@angular/forms'  
import { SharedService } from 'app/services/shared.service';
import { DataService } from 'app/services/data.service';

declare function disableButton(btn: any): any;

// Excel
import * as XLSX from 'ts-xlsx';


const input = document.getElementById('input')

@Component({
  selector: 'app-add-recipient',
  templateUrl: './add-recipient.component.html',
  styleUrls: ['./add-recipient.component.css']
})

export class AddRecipientComponent  {
  @Input()
  progress = 0;


arrayBuffer:any;
file:File;


ArrayOfRecipient= new Array();

arrayRecipient = new Array();


SurveyPost: any = [];


incomingfile(event) 
  {
  }

 Upload(event) {

  this.file= event.target.files[0]; 

     // If we Have Excel File
     if(this.file != null){
      let fileReader = new FileReader();
      fileReader.onload = (e) => {
          this.arrayBuffer = fileReader.result;
          var data = new Uint8Array(this.arrayBuffer);
          var arr = new Array();
          for(var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
          var bstr = arr.join("");
          var workbook = XLSX.read(bstr, {type:"binary"});
          var first_sheet_name = workbook.SheetNames[0];
          var worksheet = workbook.Sheets[first_sheet_name];
        
          var thisIsTestArray = new Array();

          thisIsTestArray = XLSX.utils.sheet_to_json(worksheet,{raw:true});

          thisIsTestArray.forEach(function (entry) {
           delete entry['List_1'];
         });

         for (let entry of  thisIsTestArray) {
         this.ArrayOfRecipient.push(entry['List']);
         }

         if(this.ArrayOfRecipient.length > 0){
                const value =  this.recipients().controls[0].get("qty").value;
                var RecipienList = value.concat(this.ArrayOfRecipient.toString()); 
                this.recipients().controls[0].get("qty").patchValue(RecipienList);
            }

      }
      fileReader.readAsArrayBuffer(this.file);
    }
   else{
     alert("Please Upload Excel File First");
   }

this.ArrayOfRecipient = [];
 
}




  ////
  name = 'Angular';  
    
  productForm: FormGroup;  

   // Behavior
 message:any;

 messageCode: any;
 SurveyTitle: any;

 CreatedSurveyId: any;

 ReadyJson: any;

 TitleExist:boolean = false;

 SendSurveyList: any = [];
 ////////////

  // Progress Bar
  loadingVal: number = 0;
  progressValues: any;


     
  constructor(private fb:FormBuilder, private service: SharedService, private data: DataService ) {  
     
    this.productForm = this.fb.group({  
      name: '',  
      recipients: this.fb.array([]) ,  
    });  
  }

  

   ngOnInit() {
    this.progressValues = 0;
    this.loadingVal = 0;
   // Behavior
    this.data.currentMessage.subscribe(message => this.message = message)

   }  
    
   recipients() : FormArray {  
    return this.productForm.get("recipients") as FormArray  
  }  
     
  newRecipient(): FormGroup {  
    return this.fb.group({  
      qty: '' 
    })  
  }  

  newRecipientFromArray(data: any): FormGroup {  
    return this.fb.group({  
      qty: data 
    })  
  }  
     
  addRecipient() {  
   this.recipients().push(this.newRecipient());  
  }  
     
  removeRecipient(i:number) {  
this.recipients().removeAt(i); 
  }  
     
  onSubmit() { 
    this.progressValues = 0;
    this.loadingVal = 0;


    // Email LogIns.
   var strLogIn = (<HTMLInputElement>document.getElementById("emailName")).value; 
   var strPass = (<HTMLInputElement>document.getElementById("emailPassword")).value; 

if(strLogIn == "" || strPass == ""){

  alert("Please Fill E-Mail Log In Fields");

}
else{

  if(this.productForm.value.recipients.length === 0){
    alert("Please Add Recipient");
    disableButton('addButton');
    return;
  }
  else{
  if(this.message.Code){
    if( this.message.Name ) {
      this.SurveyTitle = this.message.Name;
      this.TitleExist = !this.TitleExist;
    }
}
else if(this.message){
    const convertToObj = this.message["pages"];


    Object.keys(convertToObj[0]).forEach(key => {

      if(key == "title"){
        this.SurveyTitle = convertToObj[0].title;
        this.TitleExist = !this.TitleExist;
      }
    });
}


if(this.TitleExist === false){
  alert("Please Type The Title For Survey");
  disableButton('addButton');
        return;
}
else{
  
  this.TitleExist = !this.TitleExist;



    Object.keys(this.productForm.controls).forEach(key => {
       
          if(key === "recipients"){
          
            const recipients7 =this.productForm.value.recipients;

            for(let i=0; i<recipients7.length; i++){


              // Get Array Visu
              this.arrayRecipient = recipients7[i].qty.split(','); 

                for(let ArrayRecipientRepeat =0; ArrayRecipientRepeat< this.arrayRecipient.length; ArrayRecipientRepeat++){
              
              // if message surveyID empty not saved than
              if(this.message.SurveyId){
              }
              else{
                this.message.SurveyId = this.CreatedSurveyId;
              }
              if(this.message.Code){
                  var val = 
                  {
                    Email:this.arrayRecipient[ArrayRecipientRepeat].qty,
                    SurveyId: this.message.SurveyId,
                    SurveyCode: this.message.Code,
                    SurveyName: this.SurveyTitle,
                    EmailLogIn: strLogIn,
                    EmailPassword: strPass
                  }
              }
              else if(this.message){

                  this.messageCode = JSON.stringify(this.message);

                  var val = 
                  {
                    Email:this.arrayRecipient[ArrayRecipientRepeat].qty,
                    SurveyId: this.message.SurveyId,
                    SurveyCode:  this.messageCode,
                    SurveyName: this.SurveyTitle,
                    EmailLogIn: strLogIn,
                    EmailPassword: strPass
                  }
              }
              else{
                    alert("Firstly, Please Save The Survey");
                    disableButton('addButton');
                      return;
              }
            
              this.SendSurveyList.push(val);
            }
          }

          this.service.addRecipient(this.SendSurveyList).subscribe(res=>{});

          this.progressValues = 100 / this.arrayRecipient.length;

          for(let i=0; i< this.arrayRecipient.length; i++){
          window.setTimeout(() => (this.loadingVal += this.progressValues), i * 500);
          }
        }
      });
    }
    }
    }

    const tatalLengthRecipient = this.recipients().length;

    for (let i = 0; i < tatalLengthRecipient; i++) {
      this.recipients().removeAt(0);
    }

  }
}  
