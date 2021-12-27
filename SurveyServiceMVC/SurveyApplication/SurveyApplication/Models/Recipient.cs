using SurveyApplication.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyApplication.Models
{
    public class Recipient 
    {
        // Recipient section
        // new_recipientid
        [Required]
        public Guid? RecipientId { get; set; }


        [Required]
        public Guid? CompletedId { get; set; }

        // new_email
        [Required]
        public string Email { get; set; }

        // new_email
        [Required]
        public string Name { get; set; }

        // Many To Many
        // new_surveylookup
        [Required]
        public Guid? SurveyId { get; set; }

        [Required] // new_iscompleted
        public Boolean IsCompleted { get; set; }

        // Additional Initial Survey Code Needed
        // To create new Survey if not created
        public string SurveyCode { get; set; }
        public string SurveyName { get; set; }

        // Additional Email LogIns
        public string EmailLogIn { get; set; }
        public string EmailPassword { get; set; }


        // Process

        public Boolean ProcessSending { get; set; } 

    }
}