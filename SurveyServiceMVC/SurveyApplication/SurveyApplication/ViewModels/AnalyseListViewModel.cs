using SurveyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyApplication.ViewModels
{
    public class AnalyseListViewModel
    {
        public IEnumerable<Survey> surveys { get; set; }
        public IEnumerable<CompletedSurvey> completedSurveys { get; set; }
        public IEnumerable<Recipient> recipients{ get; set; }
    }
}