﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;

namespace SurveyApplication.Helper
{
    public class HelperClass
    {
        public static EntityCollection GetCompletedSurveyEntityCollection(CrmServiceClient service, Guid? getBySpecificId)
        {
            List<string> queryCollumns = new List<string>();

            // queryCollumns.Add("new_name");
            //queryCollumns.Add("new_code");

                    queryCollumns.Add("new_name");
                    queryCollumns.Add("new_completedsurveyid");
                    queryCollumns.Add("new_recipient");
                    queryCollumns.Add("new_survey");
                    queryCollumns.Add("new_code");
                   
            QueryExpression query = new QueryExpression("new_completedsurvey");


            // Fill Data To Query
            query.ColumnSet.AddColumns(queryCollumns.ToArray());



            // Criteria 
            query.Criteria.AddCondition("new_survey", ConditionOperator.Equal, getBySpecificId);
               
            return service.RetrieveMultiple(query);
        }
        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static EntityCollection GetEntityCollection(CrmServiceClient service, string entityName, Guid? getBySpecificId)
        {
            List<string> queryCollumns = new List<string>();

            // queryCollumns.Add("new_name");
            //queryCollumns.Add("new_code");

            switch (entityName)
            {
                case "new_survey":
                    queryCollumns.Add("new_surveyid");
                    queryCollumns.Add("new_name");
                    queryCollumns.Add("new_code");
                    break;
                case "new_recipient":
                    queryCollumns.Add("new_recipientid");
                    queryCollumns.Add("new_email");
                    queryCollumns.Add("new_survey");
                    queryCollumns.Add("new_iscompleted");
                    break;
                case "new_completedsurvey":
                    queryCollumns.Add("new_name");
                    queryCollumns.Add("new_completedsurveyid");
                    queryCollumns.Add("new_recipient");
                    queryCollumns.Add("new_survey");
                    queryCollumns.Add("new_code");
                    break;
            }

            QueryExpression query = new QueryExpression(entityName);


            // Fill Data To Query
            query.ColumnSet.AddColumns(queryCollumns.ToArray());



            // Criteria Only by Id 
            if (getBySpecificId != Guid.Empty)
            {
                // yra id

                if (entityName.Equals("new_survey"))
                {
                    query.Criteria.AddCondition("new_surveyid", ConditionOperator.Equal, getBySpecificId);
                }
                else if (entityName.Equals("new_recipient"))
                {

                    query.Criteria.AddCondition("new_recipientid", ConditionOperator.Equal, getBySpecificId);


                }
                else if (entityName.Equals("new_completedsurvey"))
                {
                    query.Criteria.AddCondition("new_completedsurveyid", ConditionOperator.Equal, getBySpecificId);
                }
            }
            else
            {

                // Just Get Survey And Display It
                if (entityName.Equals("new_survey"))
                {
                    query.Criteria.AddCondition("new_name", ConditionOperator.NotNull);
                    query.Criteria.AddCondition("new_code", ConditionOperator.NotNull);
                }
            }
            return service.RetrieveMultiple(query);
        }
    }
}