using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Chefs_Dishes.Models{

        public class MinimumAgeAttribute : ValidationAttribute{
        protected override ValidationResult IsValid (object value, ValidationContext validationContext){
            DateTime datetime = DateTime.Now;
            DateTime input = (DateTime)value;
            input = input.AddYears(18);
            System.Console.WriteLine(input);
            if(input > datetime){
                return new ValidationResult("Chef Must be Older then 18! ");
            }
            return ValidationResult.Success;
            }
        }
    }

