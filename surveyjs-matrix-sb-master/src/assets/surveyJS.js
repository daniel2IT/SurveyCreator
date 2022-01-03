
function hideSurveyJsField(){  
 
 }


function resizeInput() {
  this.style.width = this.value.length + "ch";
}


          
//$('.btn btn-primary sv-btn svd-toolbar-button svd_save_btn').onclick(function() {
//    alert("Hello");
//});


const callback = function(mutationsList, observer) {
    for(let mutation of mutationsList) {

       try {

        if (mutation.type === 'childList') {

  

            var lis3 = document.querySelector("#scrollableDiv > div > survey-widget > div > form > div.sv-container-modern > div.sv-title.sv-container-modern__title.svd_survey_header > div.sv-header__text");

            lis3.remove();

            var deleteQuer = document.querySelector("#surveyCreatorContainer > div > div > div:nth-child(1) > div > div > ul > li:nth-child(3)");

            deleteQuer.remove();

            var deleteQuer2 = document.querySelector("#surveyCreatorContainer > div > div > div:nth-child(1) > div > div > ul > li:nth-child(3)");

            deleteQuer2.remove();


            var deleteQuer3 = document.querySelector("#surveyCreatorContainer > div > div > div:nth-child(1) > div > div > ul > li:nth-child(3)");

            deleteQuer3.remove();

            var changeQuer4 = document.querySelector("#surveyCreatorContainer > div > div > div:nth-child(1) > div > div > ul > li:nth-child(2) > span");

            changeQuer4.innerHTML = "Preview";

            //var changeQuer5 = document.querySelector("#svd-save");
           // changeQuer5.header__text = "Save Survey State";

            document.getElementsByClassName("svd_commercial_container")[0].style.display = 'none';
            $('h2').remove();  
        }
    }
    catch{
        document.getElementsByClassName("sa-commercial")[0].style.display = 'none';
        document.getElementsByClassName("svd_commercial_container")[0].style.display = 'none';
        survey.haveCommercialLicense = true;
        commercial = true;
    }

    } // for 

 
   
};

// javascript call function when content changed ()
const targetNode = document.body;
const config = { childList: true, subtree: true };
const lisa = [];


const observer = new MutationObserver(callback);
observer.observe(targetNode, config);



function disableButton(btn) {
    document.getElementById(btn.id).disabled = true;
}

function enableButton(btn) {
    document.getElementById(btn.id).disabled = false;
}


var input = document.querySelector('input'); // get the input element
input.addEventListener('input', resizeInput); // bind the "resizeInput" callback on "input" event
resizeInput.call(input); // immediately call the function