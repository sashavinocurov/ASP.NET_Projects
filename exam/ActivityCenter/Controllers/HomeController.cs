using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ActivityCenter.Models;

namespace ActivityCenter.Controllers
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
            get {return HttpContext.Session.GetInt32("userid");}
            set { HttpContext.Session.SetInt32("userid", (int)value);}
        }
        [HttpGet("")]
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
            bool dupe = dbContext.Users.Any(u => u.Email == user.RegUser.Email);
            if(dupe)
            {
                ModelState.AddModelError("RegUser.Email", "A user that email already exits in database");
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
        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            if(InSession != null)
            {
                HttpContext.Session.Clear();
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            DashboardViewModel vMod = new DashboardViewModel();
            vMod.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)InSession);
            List<Act> acts = dbContext.Acts.Include(c => c.User).Include(c => c.UsersAttending).ToList();
            vMod.Act = acts.ToList();
            return View(vMod);
        }
        [HttpGet("create")]
        public IActionResult NewAct(DashboardViewModel dbvmod)
        {
            if (InSession == null)
            {
                return RedirectToAction("Index");
            }
            dbvmod.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)InSession);
            List<Act> acts = dbContext.Acts.Include(c => c.User).Include(c => c.UsersAttending).ToList();
            dbvmod.Act = acts.ToList();
            return View();
        }
        [HttpPost("newact")]
        public IActionResult CreateAct(DashboardViewModel dbvm)
        {
            if (InSession == null)
            {
                return RedirectToAction("Index");
            }
            dbvm.ActForm.UserId = (int)InSession;
            if(ModelState.IsValid)
            {
                dbContext.Add(dbvm.ActForm);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            dbvm.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)InSession);
            List<Act> acts = dbContext.Acts.Include(c => c.User).Include(c => c.UsersAttending).ToList();
            dbvm.Act = acts.ToList();
            return View("Dashboard", dbvm);
        }
        [HttpGet("act/{actId}")]
        public IActionResult OneAct(int actId)
        {
            if (InSession == null)
            {
                return RedirectToAction("Index");
            }
            Act thisOne = dbContext.Acts
                .Include(c => c.User)
                .Include(c => c.UsersAttending)
                .ThenInclude(a => a.User)
                .FirstOrDefault(c => c.ActId == actId);
            if(thisOne == null)
            {
                return RedirectToAction("Dashboard");
            }
            OneActViewModel vAMod = new OneActViewModel();
            vAMod.OneAct = thisOne;
            vAMod.LoggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == (int)InSession);

            return View(vAMod);
        }

        [HttpGet("join/{actId}")]
        public IActionResult JoinAct(int actId)
        {
            if (InSession == null)
            {
                return RedirectToAction("Index");
            }
            if(dbContext.Acts.Any(a => a.ActId == actId) && !dbContext.Associations.Any(a => (a.UserId == (int)InSession && a.ActId == actId) ))
            {
                Association toAdd = new Association();
                toAdd.UserId = (int)InSession;
                toAdd.ActId = actId;
                dbContext.Add(toAdd);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet("cancel/{actId}")]
        public IActionResult CancelAct(int actId)
        {
            if (InSession == null)
            {
                return RedirectToAction("Index");
            }
            Association toRemove = dbContext.Associations.FirstOrDefault(a => a.UserId == (int)InSession && a.ActId == actId);
            if(toRemove != null)
            {
                dbContext.Remove(toRemove);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet("delete/{actId}")]
        public IActionResult DeleteAct(int actId)
        {
            if(InSession == null)
            {
                return RedirectToAction("Index");
            }
            Act deleteAct = dbContext.Acts.FirstOrDefault(e => e.ActId == actId && e.UserId == (int)InSession);
            if(deleteAct != null)
            {
                dbContext.Remove(deleteAct);
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
