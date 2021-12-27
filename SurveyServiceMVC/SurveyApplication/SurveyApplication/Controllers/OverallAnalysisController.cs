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
    public class OverallAnalysisController : Controller
    {
        // GET: OverallAnalysis
/*        public ActionResult Index()
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

                //  Get Survey Data
                EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", Guid.Empty);

                IEnumerable<Survey> surveys = null;

                List<Survey> getListSurvey = new List<Survey>();

                // get All data -> Survey / Recipient

                foreach (var SurveyItem in surveyCollection.Entities)
                {
                    var surveyModel = new Survey();

                    surveyModel.Name = SurveyItem.Attributes["new_name"].ToString();
                    surveyModel.SurveyId = (Guid)(SurveyItem.Attributes["new_surveyid"]);

                    //  listObject.Code = completeSurveyItem.Attributes["new_code"].ToString();


                    getListSurvey.Add(surveyModel);
                }

                surveys = getListSurvey;

                var analyseSurveyObj = new AnalyseListViewModel
                {
                    surveys = surveys
                };

                // Return to View.
                return View(analyseSurveyObj.surveys);
            }
        }*/


        // GET: OverallAnalysis/Details/a2a4383f-bf4b-ec11-9119-005056010f5f
        public ActionResult ExportExcel(Guid id)
        {
            // Survey id a2a4383f-bf4b-ec11-9119-005056010f5f
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
                //  Recipient Data from completed surveyCollection where SurveyId =
                EntityCollection completeSurveyCollection = HelperClass.GetCompletedSurveyEntityCollection(service, id, "new_survey");

                if (completeSurveyCollection.Entities.Count != 0)
                {
                    // Get all Existing Result Codes
                    IEnumerable<CompletedSurvey> completedSurveys = null;

                    List<CompletedSurvey> getListCompletedSurvey = new List<CompletedSurvey>();

                    // Get Design Survey Code One Time
                    bool repeat = true;

                    // get All data -> Survey / Recipient
                    foreach (var SurveyItem in completeSurveyCollection.Entities)
                    {
                        var completedSurveyModel = new CompletedSurvey();

                        // First Repeat -> List 1 element
                        if (repeat.Equals(true))
                        {
                            // Get Survey by id code sended
                            EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", id);


                            completedSurveyModel.Name = surveyCollection.Entities[0].Attributes["new_name"].ToString();
                            completedSurveyModel.Code = surveyCollection.Entities[0].Attributes["new_code"].ToString();


                            repeat = false;
                        }


                        //   completedSurveyModel.Name = SurveyItem.Attributes["new_name"].ToString();
                        //   completedSurveyModel.SurveyId = SurveyItem.GetAttributeValue<EntityReference>("new_survey").Id;
                        // completedSurveyModel.RecipientId = SurveyItem.GetAttributeValue<EntityReference>("new_recipient").Id;

                        // Recipient Result Code
                        completedSurveyModel.ResultCode = SurveyItem.Attributes["new_code"].ToString();

                        getListCompletedSurvey.Add(completedSurveyModel);
                    }

                    completedSurveys = getListCompletedSurvey;

                    var analyseSurveyObj = new AnalyseListViewModel
                    {
                        completedSurveys = completedSurveys
                    };

                    // Return to View.
                    return View(analyseSurveyObj.completedSurveys);
                }
                else
                {

                    EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", id);



                    // Get all Existing Result Codes
                    IEnumerable<CompletedSurvey> completedSurveys = null;

                    List<CompletedSurvey> getListCompletedSurvey = new List<CompletedSurvey>();

                    // get All data -> Survey / Recipient

                    var completedSurveyModel = new CompletedSurvey();

                    // Recipient Result Code
                    completedSurveyModel.Code = surveyCollection.Entities[0].Attributes["new_code"].ToString();



                    getListCompletedSurvey.Add(completedSurveyModel);




                    completedSurveys = getListCompletedSurvey;

                    var analyseSurveyObj = new AnalyseListViewModel
                    {
                        completedSurveys = completedSurveys
                    };


                    return View(analyseSurveyObj.completedSurveys);

                }
            }
        }


        // GET: OverallAnalysis/Details/a2a4383f-bf4b-ec11-9119-005056010f5f
        public ActionResult Details(Guid id)
        {
               // Survey id a2a4383f-bf4b-ec11-9119-005056010f5f
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
                      //  Recipient Data from completed surveyCollection where SurveyId =
                      EntityCollection completeSurveyCollection = HelperClass.GetCompletedSurveyEntityCollection(service, id, "new_survey");

                      if (completeSurveyCollection.Entities.Count != 0)
                      {
                          // Get all Existing Result Codes
                          IEnumerable<CompletedSurvey> completedSurveys = null;

                          List<CompletedSurvey> getListCompletedSurvey = new List<CompletedSurvey>();

                          // Get Design Survey Code One Time
                          bool repeat = true;

                          // get All data -> Survey / Recipient
                          foreach (var SurveyItem in completeSurveyCollection.Entities)
                          {
                              var completedSurveyModel = new CompletedSurvey();

                              // First Repeat -> List 1 element
                              if (repeat.Equals(true))
                              {
                                  // Get Survey by id code sended
                                  EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", id);


                                  completedSurveyModel.Code = surveyCollection.Entities[0].Attributes["new_code"].ToString();


                                  repeat = false;
                              }


                           //   completedSurveyModel.Name = SurveyItem.Attributes["new_name"].ToString();
                           //   completedSurveyModel.SurveyId = SurveyItem.GetAttributeValue<EntityReference>("new_survey").Id;
                             // completedSurveyModel.RecipientId = SurveyItem.GetAttributeValue<EntityReference>("new_recipient").Id;

                              // Recipient Result Code
                              completedSurveyModel.ResultCode = SurveyItem.Attributes["new_code"].ToString();

                              getListCompletedSurvey.Add(completedSurveyModel);
                          }

                          completedSurveys = getListCompletedSurvey;

                          var analyseSurveyObj = new AnalyseListViewModel
                          {
                              completedSurveys = completedSurveys
                          };

                          // Return to View.
                          return View(analyseSurveyObj.completedSurveys);
                      }
                      else
                      {

                          EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", id);



                          // Get all Existing Result Codes
                          IEnumerable<CompletedSurvey> completedSurveys = null;

                          List<CompletedSurvey> getListCompletedSurvey = new List<CompletedSurvey>();

                          // get All data -> Survey / Recipient

                              var completedSurveyModel = new CompletedSurvey();

                              // Recipient Result Code
                              completedSurveyModel.Code = surveyCollection.Entities[0].Attributes["new_code"].ToString();



                              getListCompletedSurvey.Add(completedSurveyModel);




                          completedSurveys = getListCompletedSurvey;

                          var analyseSurveyObj = new AnalyseListViewModel
                          {
                              completedSurveys = completedSurveys
                          };


                          return View(analyseSurveyObj.completedSurveys);

                      }

                  }
        }

        // GET: OverallAnalysis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OverallAnalysis/Create
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

        // GET: OverallAnalysis/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OverallAnalysis/Edit/5
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

        // GET: OverallAnalysis/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OverallAnalysis/Delete/5
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
