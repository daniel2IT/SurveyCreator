using Microsoft.Xrm.Sdk;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (Entity expense in surveyCollection.Entities)
            {
                Survey expenseModel = new Survey();

                // Get Employee Id&&Name
                //expenseModel.SurveyId = (Guid)((EntityReference)expense.Attributes["new_survey"]).Id;
                expenseModel.SurveyId = (Guid)expense.Attributes["new_surveyid"];


                expenseModel.Name = expense.Attributes["new_name"].ToString();
                expenseModel.Code = expense.Attributes["new_code"].ToString();


                surveyList.Add(expenseModel);
            }
            return surveyList;
        }
    }
}