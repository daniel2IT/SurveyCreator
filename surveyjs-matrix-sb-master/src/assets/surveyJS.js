
function hideSurveyJsField(){  
 
 }



// javascript call function when content changed ()
const targetNode = document.body;
const config = { childList: true, subtree: true };
const lisa = [];

          
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


          document.getElementsByClassName("svd_commercial_container")[0].style.display = 'none';
          $('h2').remove();   
          

        }
    }
    catch{
        document.getElementsByClassName("sa-commercial")[0].style.display = 'none';
    }

    } // for 

 
   
};

const observer = new MutationObserver(callback);
observer.observe(targetNode, config);