using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltPrep.Models
{
    public class Happening
    {
        [Key]
        public int HappeningId { get; set; }
        [Required(ErrorMessage = "Enter name event!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter date")]
        [ActivityDateTime]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Must enter Location")]
        public string City { get; set; }
        [Required(ErrorMessage = "Must Select State")]
        public string State { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Association> UsersAttending {get; set; }
        public List<Comment> CommentsOnEvent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }

    public class ActivityDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime start = (DateTime)value;
            Console.WriteLine("[START DATETIME]==>" + start);
            if((DateTime.Now - start).TotalMinutes > 0)
            {
                return new ValidationResult("Time Travel Has yet to be invented");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}