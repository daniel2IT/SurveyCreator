using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using SurveyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Interfaces
{
    public interface ISurveyRepository
    {
        IEnumerable<Survey> GetSurvey(EntityCollection surveyCollection);

        IEnumerable<Recipient> GetRecipient(EntityCollection surveyCollection, CrmServiceClient service);

        void DeleteRecipient(EntityCollection recipientCollection, Guid id, CrmServiceClient service);

        Entity CreateSurvey(Survey postSurvey);

        Entity CreateRecipient(Recipient postRecipient);

        // Analyses
        IEnumerable<CompletedSurvey> GetCompletedSurvey(EntityCollection completedSurveyCollection);
    }
}
