
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
        if (mutation.type === 'childList') {
          document.getElementsByClassName("svd_commercial_container")[0].style.display = 'none';
          $('h2').remove();     


    var lis1 = document.querySelector("html body app-root div#app creator-page div.container- survey-creator div#surveyCreatorContainer div.svd_container.svd-light-bg-color.sv_modern_css div.svd-vertical-container div.svd-vertical-container__row div.svd-vertical-container__cell div ul.navbar-default.container-fluid.nav.nav-tabs.editor-tabs.svd-tabs.svd-light-bg-color");
   
    for(var i=8; i < lis1.childElementCount + 12; i++) {
            lis1.removeChild(lis1.childNodes[i]);
            }
        }
    }
};

const observer = new MutationObserver(callback);
observer.observe(targetNode, config);