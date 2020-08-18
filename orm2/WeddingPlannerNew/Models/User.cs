using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class User{
        [Key]
            public int UserId { get; set; }
            [Required]
            [MinLength(2)]
            public string FirstName { get; set; }
            [Required]
            [MinLength(2)]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            public string Email{ get; set; }
            [Required]
            [DataType(DataType.Password)]
            [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            public string Password { get; set; }
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            [NotMapped]
            [Compare("Password")]
            [DataType(DataType.Password)]
            public string Confirm {get;set;}
            public List<RSVP> RSVPs {get;set;}
            public List<Wedding> CreatedWeddings {get;set;}
    }
}