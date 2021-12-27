using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using SurveyApplication.Helper;
using SurveyApplication.Models;
using SurveyApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyApplication.Controllers
{
    public class AnalyserController : Controller
    {

        // GET: Analyser
        public ActionResult Index()
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
                EntityCollection completeSurveyCollection = HelperClass.GetEntityCollection(service, "new_completedsurvey", Guid.Empty);


                IEnumerable<CompletedSurvey> completedSurveys = null;

                List<CompletedSurvey> getListCompleteSurvey = new List<CompletedSurvey>();

                // get All data -> Survey / Recipient
                foreach (var completeSurveyItem in completeSurveyCollection.Entities)
                {
                    var listObject = new CompletedSurvey();


                    listObject.Name = completeSurveyItem.Attributes["new_name"].ToString();
                    listObject.CompletedSurveyId= (Guid)(completeSurveyItem.Attributes["new_completedsurveyid"]);

                    

                    //  listObject.Code = completeSurveyItem.Attributes["new_code"].ToString();


                    getListCompleteSurvey.Add(listObject);
                }


                completedSurveys = getListCompleteSurvey;

                //IEnumerable<Survey> surveys = null;
                // IEnumerable<Recipient> recipients = null;

                var analyseSurveyObj = new AnalyseListViewModel
                {
                  //  surveys = surveys,
                  //  recipients = recipients,
                    completedSurveys = completedSurveys
                };

                // Return to View.
                return View(analyseSurveyObj.completedSurveys);
            }
        }

        // GET: Analyser/Details/88E860FE-AE53-EC11-911B-005056010F5F
        // 88E860FE-AE53-EC11-911B-005056010F5F
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
                try { 
                //  Recipient Data
                EntityCollection completeSurveyCollection = HelperClass.GetCompletedSurveyEntityCollection(service, id, "new_recipient");


                // Get completeSurveyCollection -> survey 
                var surveyId = completeSurveyCollection.Entities[0].GetAttributeValue<EntityReference>("new_survey").Id;

                // Get completeSurveyCollection -> recipient 
                var recipientId = completeSurveyCollection.Entities[0].GetAttributeValue<EntityReference>("new_survey").Id;

                var analyseResultCode = completeSurveyCollection.Entities[0].Attributes["new_code"].ToString();


                // Survey Data
                EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", surveyId);

                var surveyCode = surveyCollection.Entities[0].Attributes["new_code"].ToString();



                var surveyObj = new CompletedSurvey
                {
                    SurveyId = surveyId,
                    CompletedSurveyId = id,
                    ResultCode = analyseResultCode,
                    RecipientId = id,
                    Code = surveyCode
                };


                // Return to View.
                return View(surveyObj);
                }
                catch(Exception ex)
                {
                    throw new ArgumentException("The respondent did not fill out the questionnaire yet");
                }
            }

        }

        // GET: Analyser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Analyser/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Analyser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Analyser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Analyser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Analyser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
