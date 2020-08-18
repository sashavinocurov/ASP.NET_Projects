using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltPrep.Models
{
    public class Comment
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int HappeningId { get; set; }
        public User User { get; set; }
        public Happening Happening { get; set; }
        [Required]
        public string Message { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}