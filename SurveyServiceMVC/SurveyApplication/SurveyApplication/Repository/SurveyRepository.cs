using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using SurveyApplication.Helper;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SurveyApplication.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        public Entity CreateRecipient(Recipient postRecipient)
        {

            Entity recipient = new Entity("new_recipient");


            recipient["new_email"] = postRecipient.Email;

            recipient["new_iscompleted"] = false;

            // new_surveylookup
            recipient["new_survey"] = new EntityReference("new_survey", (Guid)postRecipient.SurveyId);

            return recipient;
        }

        public Entity CreateSurvey(Survey postSurvey)
        {
            Entity survey = new Entity("new_survey");


            survey["new_name"] = postSurvey.Name;

            survey["new_code"] = postSurvey.Code;

          //  survey["new_recipient"] = new EntityReference("new_recipient", (Guid)postSurvey.RecipientId);


            return survey;
        }

        public IEnumerable<CompletedSurvey> GetCompletedSurvey(EntityCollection completedSurveyCollection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Survey> GetSurvey(EntityCollection surveyCollection)
        {
            List<Survey> surveyList = new List<Survey>();

            foreach (Entity suvey in surveyCollection.Entities)
            {
                Survey surveyModel = new Survey();

                surveyModel.SurveyId = (Guid)suvey.Attributes["new_surveyid"];
                surveyModel.Name = suvey.Attributes["new_name"].ToString();
                surveyModel.Code = suvey.Attributes["new_code"].ToString();

                surveyList.Add(surveyModel);
            }
            return surveyList;
        }



        public IEnumerable<Recipient> GetRecipient(EntityCollection recipientCollection, CrmServiceClient service)
        {
            List<Recipient> recipientList = new List<Recipient>();

            foreach (Entity recipient in recipientCollection.Entities)
            {
                Recipient recipientModel = new Recipient();

                // Get Recipient Id
                 recipientModel.RecipientId = (Guid)recipient.Attributes["new_recipientid"];

                // Get Survey Name
                EntityReference surveyReference = (EntityReference)recipient.Attributes["new_survey"];
                recipientModel.SurveyName = surveyReference.Name;

                // Get Username From Email
                MailAddress addr = new MailAddress(recipient.Attributes["new_email"].ToString());
                string username = addr.User;
                recipientModel.Name = username;

                // If Recipient Completed Survey Get:
                EntityCollection completeSurveyCollection = HelperClass.GetEntityCollection(service, "new_completedsurvey", recipientModel.RecipientId);

                if (completeSurveyCollection.Entities.Count != 0)
                {
                    recipientModel.CompletedId = (Guid)completeSurveyCollection.Entities[0].Id;
                }


                recipientList.Add(recipientModel);
            }
            return recipientList;
        }

        public void DeleteRecipient(EntityCollection recipientCollection, Guid id, CrmServiceClient service)
        {





            throw new NotImplementedException();
        }
    }
}