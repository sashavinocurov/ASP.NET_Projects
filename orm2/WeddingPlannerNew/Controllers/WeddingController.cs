using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers{
    public class WeddingController : Controller{
        private MyContext dbContext;
        public WeddingController(MyContext context){
            dbContext = context;
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard(){
            if(HttpContext.Session.GetInt32("userid") == null){
                return RedirectToAction("Index", "Home");
            }
            int userIdsession = (int)HttpContext.Session.GetInt32("userid");
            User loggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == userIdsession);
            List<Wedding> allWedd = dbContext.Weddings
            .Include(w=>w.RSVPs)
            .ThenInclude(r=>r.Guest)
            .ToList();
            DisplayWeddingView newView = new DisplayWeddingView();
            newView.user = loggedUser;
            newView.weddings = allWedd;
            return View("Dashboard", newView);
        }
        [HttpPost("addnewwedding/{id}")]
        public IActionResult AddNewWedding(Wedding newWedding, int id){
            if(ModelState.IsValid){
                newWedding.UserId = id;
                dbContext.Weddings.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard", "Wedding");}
            else{
                return View("NewWedding");
            }
        }
        [HttpGet("newwedding/{id}")]
        public IActionResult DiplayNewWedding(int id){
            ViewBag.user = id;
            return View("NewWedding");
        }
        [HttpGet("displaywedding/{id}")]
        public IActionResult DisplayWedding(int id){
            Wedding oneWedd = dbContext.Weddings
            .Include(r=>r.RSVPs)
            .ThenInclude(rsv=>rsv.Guest)
            .FirstOrDefault(p=>p.WeddingId == id);
            return View("DisplayWedding", oneWedd);
        }
        [HttpGet("delete/{id}")]
        public IActionResult DeleteWedding(int id){
            Wedding deleteWedd = dbContext.Weddings.SingleOrDefault(w=>w.WeddingId == id);
            dbContext.Weddings.Remove(deleteWedd);
            dbContext.SaveChanges();
            return RedirectToAction ("Dashboard", "Wedding");
        }
        [HttpGet("reserve/{id}")]
        public IActionResult AddToGuest(int id){
            RSVP addGuest = new RSVP();
            int userIdSession = (int)HttpContext.Session.GetInt32("userid"); 
            addGuest.UserId = userIdSession;
            addGuest.WeddingId = id;
            dbContext.RSVPs.Add(addGuest);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard", "Wedding");
        }
        [HttpGet("unreserve/{id}")]
        public IActionResult RemoveFromGuest(int id){
            int userIdSession = (int)HttpContext.Session.GetInt32("userid");
            RSVP del = dbContext.RSVPs.FirstOrDefault(r=>r.UserId == userIdSession && r.WeddingId == id);
            dbContext.RSVPs.Remove(del);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard", "Wedding");
        }
    }

}
