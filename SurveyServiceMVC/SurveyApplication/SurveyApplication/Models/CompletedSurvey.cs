using SurveyApplication.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyApplication.Models
{
    public class CompletedSurvey
    {
        [Required]
        public Guid CompletedSurveyId { get; set; }

        // Recipient section
        [Required]
        public Guid RecipientId { get; set; }

        [Required]
        public Guid SurveyId { get; set; }

        [Required]
        public string ResultCode { get; set; }

        [Required]
        public string Name { get; set; }

        // Additional variable for results
        public string Code { get; set; }
    }
}