using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Login.Models
{
    public class IndexViewModel{
        public User user { get; set; }
        public int UserId { get; set; }
    }
}