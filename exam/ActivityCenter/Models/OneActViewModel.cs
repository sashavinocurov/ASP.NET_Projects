using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class OneActViewModel
    {
        public User LoggedUser { get; set; }
        public Act OneAct { get; set; }
    }
}