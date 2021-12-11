using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Newtonsoft.Json.Linq;
using SurveyApplication.Helper;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyApplication.Controllers
{
    public class CompleteSurveyController : Controller
    {
        // GET: CompleteSurvey
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompleteSurvey/Details/577209C9-7F52-EC11-911B-005056010F5F
        // [value("9245fe4a-d402-451c-b9ed-9c1a04247482")]
        public ActionResult Details(Guid id)
        {

            NetworkCredential myCred = new NetworkCredential("dev\\daniel.vaskevic", "Qwertas2235563", "");
            using (var service = new CrmServiceClient(myCred,
                                                  Microsoft.Xrm.Tooling.Connector.AuthenticationType.IFD,
                                                  "lithgrad2.wdx-dev.net",
                                                  "",
                                                  "LithGrad2",
                                                  false,
                                                  true,
                                                  null))
            {


                //  Recipient Data
                EntityCollection recipientCollection = HelperClass.GetEntityCollection(service, "new_recipient", id);

                // Check Recipient // new_iscompleted
                var isCompleted = recipientCollection.Entities[0].GetAttributeValue<bool>("new_iscompleted");

                if (isCompleted.Equals(false))
                {

                    // Get recipient -> survey 
                    var surveyId = recipientCollection.Entities[0].GetAttributeValue<EntityReference>("new_survey").Id;


                    // Get Survey data
                    EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", surveyId);

                    var SurveyName = surveyCollection.Entities[0].Attributes["new_name"].ToString();
                    var SurveyCode = surveyCollection.Entities[0].Attributes["new_code"].ToString();


                    var surveyObj = new Survey
                    {
                        SurveyId = surveyId,
                        Name = SurveyName,
                        Code = SurveyCode,
                        RecipientId = id
                    };


                    // Return to View.
                    return View(surveyObj);

                }

        

                // Return to View.
                return View();
            }
        }

        // GET: CompleteSurvey/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompleteSurvey/Edit/5
        [HttpPost]
        public ActionResult GetSurveyCompletedCode(Survey model)
        {
            try
            {


                NetworkCredential myCred = new NetworkCredential("dev\\daniel.vaskevic", "Qwertas2235563", "");
                using (var service = new CrmServiceClient(myCred,
                                                      Microsoft.Xrm.Tooling.Connector.AuthenticationType.IFD,
                                                      "lithgrad2.wdx-dev.net",
                                                      "",
                                                      "LithGrad2",
                                                      false,
                                                      true,
                                                      null))
                {
                    // change(Edit) IsCompleted = true;
                    // Edit Action for Recipient
                    Entity entityRecipient = new Entity("new_recipient", (Guid)model.RecipientId);
                    entityRecipient["new_iscompleted"] = true;
                    service.Update(entityRecipient);


                    // Now we need to save Recipient Questionaire Answers
                    // Create in Db
                    Entity completedSurveyEntity = new Entity("new_completedsurvey");


                    completedSurveyEntity["new_name"] = model.Name;


                    completedSurveyEntity["new_code"] = model.Code;

                    // new_surveylookup
                    completedSurveyEntity["new_survey"] = new EntityReference("new_survey", (Guid)model.SurveyId);

                    // recipientlookup
                    completedSurveyEntity["new_recipient"] = new EntityReference("new_recipient", (Guid)model.RecipientId);


                    service.Create(completedSurveyEntity);

                    return new HttpStatusCodeResult(204);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
