using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltPrep.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "First Name Is Required.")]
        [MinLength(2, ErrorMessage = "First Name Required 2 or more Characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required.")]
        [MinLength(2, ErrorMessage = "Last Name Required 2 or more Characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You Must enter your City name")]
        [MinLength(3, ErrorMessage = "City length should be 3 Characters or More")]
        public string City { get; set; }
        [Required(ErrorMessage = "You must enter your State Name")]
        [MinLength(2, ErrorMessage = "Only 2 Characters")]
        [MaxLength(2, ErrorMessage = "Only 2 Characters")]
        public string State { get; set; }
        [Required(ErrorMessage = "Email Is Required.")]
        [EmailAddress(ErrorMessage = "You Must Enter Valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password Required 6 or more Characters")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        [Required(ErrorMessage = "Confirm your passsword.")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now; 
        public DateTime UpdatedAt{ get; set; } = DateTime.Now; 
        public List<Happening> EventsCreated { get; set; }
        public List<Association> EventsAttending { get; set; }
        public List<Comment> CommentsMade { get; set; }
    }
}
