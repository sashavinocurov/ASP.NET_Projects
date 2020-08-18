using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltPrep.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "You must enter an Email Address")]
        [EmailAddress(ErrorMessage = "You Must enter a valid Email Address")]
        public string LogEmail { get; set; }
        [Required(ErrorMessage = "You must enter a Password")]
        [MinLength(6, ErrorMessage = "Password must be longer then 6 characters")]
        public string LogPassword { get; set; }

    }
}