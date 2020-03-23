﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyResult
    {
        public int SurveyId { get; set; }
        [Required(ErrorMessage = "Please select a park")]
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "Please enter a valid email address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please select your state of residence")]
        public string State { get; set; }

        [Required (ErrorMessage = "Please select an activity")]
        public string ActivityLevel { get; set; }

    }
}
