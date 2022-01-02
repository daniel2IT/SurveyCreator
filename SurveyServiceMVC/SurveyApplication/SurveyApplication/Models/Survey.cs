using SurveyApplication.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyApplication.Models
{
    public class Survey
    {
        // Survey section
        [Required]
        public Guid? SurveyId { get; set; }
      //  [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }

        // Additional Just For CompleteSurveyController Declaration.
        public Guid? RecipientId { get; set; }
    }
}