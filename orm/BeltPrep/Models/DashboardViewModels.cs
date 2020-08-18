using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltPrep.Models
{
    public class DashboardViewModel
    {
        public User LoggedUser { get; set; }
        public List<Happening> EventsInState { get; set; }
        public List<Happening> EventsOutOfState { get; set; }
        public Happening EventForm { get; set; }
    }
}