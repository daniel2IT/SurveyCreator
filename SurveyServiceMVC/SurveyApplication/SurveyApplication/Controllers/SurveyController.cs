using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using SurveyApplication.Helper;
using SurveyApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurveyApplication.Controllers
{
    public class SurveyController : ApiController
    {
        private readonly ISurveyRepository _repository;
        public SurveyController(ISurveyRepository repository)
        {
            _repository = repository;
        }


        // GET
        [HttpGet]
        public HttpResponseMessage Get()
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

                    // Get Collection Data
                    EntityCollection surveyCollection = HelperClass.GetEntityCollection(service, "new_survey", Guid.Empty);

                    return Request.CreateResponse(HttpStatusCode.OK, _repository.GetSurvey(surveyCollection));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // GET: Survey/Delete/5
        [HttpDelete]
        public string Delete(Guid Id)
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


                    // All this survey -> recipients
                    EntityCollection recipientCollection = HelperClass.GetEntityCollectionRecipientsBySurveyId(service, Id);

                    // All completedSurveys -> recipients(this survey)
                    foreach (Entity recipient in recipientCollection.Entities)
                    {
                        
                        EntityCollection completeSurveyCollection = HelperClass.GetCompletedSurveyEntityCollection(service, recipient.Id, "new_recipient");

                        // Delete Recipient - Completed Surveys
                        if (completeSurveyCollection.Entities.Count != 0)
                        {
                            service.Delete("new_completedsurvey", (Guid)completeSurveyCollection.Entities[0].Id);
                        }
                    }

                    int recipientCount = recipientCollection.Entities.Count();

                    for (int i = 0; i < recipientCount; i++)
                    {
                        // Delete recipients
                        service.Delete("new_recipient", (Guid)recipientCollection.Entities[0].Id);
                        recipientCollection.Entities.RemoveAt(0);
                    }


                    // Delete Survey
                    service.Delete("new_survey", Id);


                    return "Record Successfully Deleted";
                }

            }
            catch
            {
                return "Failed to delete";
            }
        }
    }
}
