using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class Act
    {
        [Key]
        public int ActId { get; set; }
        [Required(ErrorMessage = "Enter Name of the Activity.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Date of the Activity.")]
        [ActivityDateTime]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Time of an Activity is Required.")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Must Specify Duration of the Activity.")]
        [Range(1,200, ErrorMessage = "Excseed Duration Limits.")]
        public int Duration { get; set; }
        [Required(ErrorMessage = "Time type should be specified.")]
        public string TimeType { get; set; }
        [Required(ErrorMessage = "Description of Activity should be specified.")]
        [MinLength(5, ErrorMessage = "Description should be longer then 5 Characters")]
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Association> UsersAttending { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now; 
        public DateTime UpdatedAt{ get; set; } = DateTime.Now;
    }




    public class ActivityDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime start = (DateTime)value;
            Console.WriteLine("[START DATETIME]==>" + start);
            if((DateTime.Now - start).TotalMinutes > 0)
            {
                return new ValidationResult("Cant Enter Activity that Already Passed");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}