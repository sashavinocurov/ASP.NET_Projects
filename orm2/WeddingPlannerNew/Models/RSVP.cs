using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace WeddingPlanner.Models{
    public class RSVP{
        public int RsvpId {get;set;}
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        public User Guest {get;set;}
        public Wedding AttendedWedding {get;set;}
    }
}