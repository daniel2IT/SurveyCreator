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



    }
}
