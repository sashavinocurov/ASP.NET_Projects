using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ActId { get; set; }
        public User User { get; set; }
        public Act Act { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.Now; 
        public DateTime UpdatedAt{ get; set; } = DateTime.Now;
    }
}