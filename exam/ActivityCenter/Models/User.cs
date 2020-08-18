using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name Is Required.")]
        [MinLength(2, ErrorMessage = "Name Required 2 or more Characters.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email Is Required.")]
        [EmailAddress(ErrorMessage = "You Must Enter Valid Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password Required 8 or more Characters.")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        [Required(ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now; 
        public DateTime UpdatedAt{ get; set; } = DateTime.Now;
    }
} 