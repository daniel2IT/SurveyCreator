using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using SurveyApplication.Helper;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace SurveyApplication.Controllers
{
    public class RecipientController : ApiController
    {

        private readonly ISurveyRepository _repository;
        public RecipientController(ISurveyRepository repository)
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
                    EntityCollection recipientCollection = HelperClass.GetEntityCollection(service, "new_recipient", Guid.Empty);

                    return Request.CreateResponse(HttpStatusCode.OK, _repository.GetRecipient(recipientCollection, service));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static string oldCode;


        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody] List<Recipient> newRecipientsList)
        {
            foreach (var postRecipient in newRecipientsList)
            {
                if (postRecipient.Email != null || postRecipient.Email != "")
                {
                    if (HelperClass.IsValidEmail(Convert.ToString(postRecipient.Email)).Equals(false))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }

                if (postRecipient.SurveyCode != null)
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
                        // SEND EMAIL
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(postRecipient.EmailLogIn, postRecipient.EmailPassword),
                            EnableSsl = true,
                        };

                        // Is survey to be save exists at all ?
                        if (postRecipient.SurveyId == Guid.Empty || postRecipient.SurveyId.Equals(null))
                        {

                            if (oldCode != postRecipient.SurveyCode)
                            {
                                oldCode = postRecipient.SurveyCode;
                            }

                            // Check Survey Code Existing 
                            EntityCollection GetSurveyByCodeEntityCollection = HelperClass.GetSurveyByCodeEntityCollection(service, postRecipient.SurveyCode);

                            if (GetSurveyByCodeEntityCollection.Entities.Count == 0)
                            {
                                // Survey Data
                                var surveyObj = new Survey
                                {
                                    Code = postRecipient.SurveyCode,
                                    Name = postRecipient.SurveyName, // get title from angular
                                    RecipientId = postRecipient.RecipientId
                                };

                                // Create Survey
                                postRecipient.SurveyId = service.Create(_repository.CreateSurvey(surveyObj));
                            }
                            else
                            {
                                postRecipient.SurveyId = GetSurveyByCodeEntityCollection.Entities[0].Id;
                            }
                        }

                        // Create Recipient In db
                        postRecipient.RecipientId = service.Create(_repository.CreateRecipient(postRecipient));


                        // Send Message With Link
                        // https://localhost:44341/CompleteSurvey/Details/
                        smtpClient.Send("areszka789@gmail.com", postRecipient.Email, "WDXSurvey", "https://localhost:44341/CompleteSurvey/Details/" + Convert.ToString(postRecipient.RecipientId));
                    }
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.OK, ModelState);
        }



        // GET: Analyser/Delete/5
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

                    service.Delete("new_recipient", Id);

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
