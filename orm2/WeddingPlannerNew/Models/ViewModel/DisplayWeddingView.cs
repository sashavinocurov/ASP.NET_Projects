using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models{
    public class DisplayWeddingView{
    public User user {get;set;}
    public List<Wedding> weddings {get;set;}
    public int UserId {get;set;}
    }
}