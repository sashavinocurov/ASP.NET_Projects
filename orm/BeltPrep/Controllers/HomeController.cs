using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeltPrep.Models;

namespace BeltPrep.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        public int? InSession
        {
           get { return HttpContext.Session.GetInt32("userid");}
           set { HttpContext.Session.SetInt32("userid", (int)value);} 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LogRegViewModel user)
        {
            User inDb = dbContext.Users.FirstOrDefault(u => u.Email == user.LogUser.LogEmail);
            if (ModelState.IsValid && inDb != null)
            {
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                if (hasher.VerifyHashedPassword(user.LogUser, inDb.Password, user.LogUser.LogPassword) !=0)
                {
                    InSession = inDb.UserId;
                    return RedirectToAction("Dashboard");
                }
                ModelState.TryAddModelError("LogUser.Email", "Invalid email/password combination");
            }
            return View("Index", user);
        }

        [HttpPost("register")]
        public IActionResult Register(LogRegViewModel user)
        {
            List<string> stateList = new List<string>();
            stateList.Add("WA");
            stateList.Add("CA");
            stateList.Add("CO");
            stateList.Add("NY");
            stateList.Add("ID");
            stateList.Add("AR");
            stateList.Add("DC");
            stateList.Add("KS");
            stateList.Add("MA");
            bool dupe = dbContext.Users.Any(u => u.Email == user.RegUser.Email);
            if(dupe)
            {
                ModelState.AddModelError("RegUser.Email", "A user that email already exits in database");
            }
            else if(!stateList.Contains(user.RegUser.State))
            {
                dupe = true; 
                ModelState.AddModelError("RegUser.State", "Stop hacking you!");
            }
            else if(ModelState.IsValid && !dupe)
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                user.RegUser.Password = hasher.HashPassword(user.RegUser, user.RegUser.Password);
                dbContext.Users.Add(user.RegUser);
                dbContext.SaveChanges();
                InSession = user.RegUser.UserId;
                return RedirectToAction("Dashboard");
            }
            return View("Index", user);
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (InSession == null)
            {
                return RedirectToAction("Index");
            }
            DashboardViewModel vMod = new DashboardViewModel();
            vMod.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)InSession);
            List<Happening> events = dbContext.Happenings.Include(h => h.User).Include(h => h.UsersAttending).ToList();
            vMod.EventsInState = events.Where(e => e.State == vMod.LoggedUser.State).ToList();
            vMod.EventsOutOfState = events.Where(e => e.State != vMod.LoggedUser.State).ToList();
            return View(vMod);
        }
        [HttpPost("newevent")]
        public IActionResult NewEvent(DashboardViewModel dbvm)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            dbvm.EventForm.UserId = (int)InSession;
            if(ModelState.IsValid)
            {
                dbContext.Add(dbvm.EventForm);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            dbvm.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)InSession);
            List<Happening> events = dbContext.Happenings.Include(h => h.User).Include(h => h.UsersAttending).ToList();
            dbvm.EventsInState = events.Where(e => e.State == dbvm.LoggedUser.State).ToList();
            dbvm.EventsOutOfState = events.Where(e => e.State != dbvm.LoggedUser.State).ToList();
            return View("Dashboard", dbvm);
        }

        [HttpGet("join/{eventId}")]
        public IActionResult JoinHappening(int eventId)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            if(dbContext.Happenings.Any(a => a.HappeningId == eventId) && !dbContext.Associations.Any(a => (a.UserId == (int)InSession && a.HappeningId == eventId) || (a.HappeningId == eventId)))
            {
                Association toAdd = new Association();
                toAdd.UserId = (int)InSession;
                toAdd.HappeningId = eventId;
                dbContext.Add(toAdd);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet("cancel/{eventId}")]
        public IActionResult CancelHappening(int eventId)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            Association toRemove = dbContext.Associations.FirstOrDefault(a => a.UserId == (int)InSession && a.HappeningId == eventId);
            if(toRemove != null)
            {
                dbContext.Remove(toRemove);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet("edit/{eventId}")]
        public IActionResult EventToEdit(int eventId)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            Happening toEdit = dbContext.Happenings.FirstOrDefault(e => e.HappeningId == eventId);
            if(toEdit.UserId == (int)InSession && toEdit != null)
            {
                return View(toEdit);
            }
            return RedirectToAction("Dashboard");
        }
        [HttpPost("edit/{eventId}")]
        public IActionResult EditEvent(int eventId, Happening toEdit)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            
            if(dbContext.Happenings.Any(e => e.UserId == (int)InSession && e.HappeningId == eventId) && ModelState.IsValid)
            {
                toEdit.HappeningId = eventId;
                toEdit.UserId = (int)InSession;
                dbContext.Update(toEdit);
                dbContext.Entry(toEdit).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet("delete/{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            Happening deleteIdk = dbContext.Happenings.FirstOrDefault(e => e.HappeningId == eventId && e.UserId == (int)InSession);
            if(deleteIdk != null)
            {
                dbContext.Remove(deleteIdk);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
