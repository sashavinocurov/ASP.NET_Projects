using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class DashboardViewModel
    {
        public User LoggedUser { get; set; }
        public List<Act> Act { get; set; }
        public Act ActForm { get; set; }
    }
}