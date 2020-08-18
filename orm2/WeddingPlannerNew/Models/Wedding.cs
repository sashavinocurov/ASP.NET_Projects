using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class Wedding{
        public int WeddingId {get;set;}
        [Required]
        [MinLength(2)]
        public string WedderOne {get;set;}
        [Required]
        [MinLength(2)]
        public string WedderTwo {get;set;}
        [Required]
        [FutureDate]
        public DateTime DateOfWedding {get;set;}
        [Required]
        public string WeddingAddress {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<RSVP> RSVPs {get;set;}
        public int UserId {get;set;}
        public User Creator {get;set;}

    }

    public class FutureDateAttribute : ValidationAttribute{
        protected override ValidationResult IsValid (object value, ValidationContext validationContext){
            DateTime datetime = DateTime.Now;
            DateTime input = (DateTime)value;
            if(input < datetime){
                return new ValidationResult("Wedding must be in future!");
            }
            return ValidationResult.Success;
            }
        }
}