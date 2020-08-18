using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context){
            dbContext = context;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User newUser){
            if(ModelState.IsValid){
                if(dbContext.Users.Any(u => u.Email == newUser.Email)){
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("index");
                }
                PasswordHasher <User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                User lastUser = dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
                HttpContext.Session.SetInt32("userid", lastUser.UserId);
                return RedirectToAction ("Dashboard", "Wedding");//Change redirect to!!!
                }
            else{
                return View("Index");
            }
        }
        [HttpGet("loginpage")]
        public IActionResult LoginPage(){
            return View ("Login");
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser userSubmit){
            if(ModelState.IsValid){
                var UserInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmit.Email);
                if(UserInDb == null){
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmit, UserInDb.Password, userSubmit.Password);
                if(result == 0){
                    ModelState.AddModelError("Password", "Invalid Email/Password");
                    return View("Login");
                    }
                User loggedUser = dbContext.Users.FirstOrDefault(u => u.Email == userSubmit.Email);
                HttpContext.Session.SetInt32("userid", loggedUser.UserId);
                return RedirectToAction("Dashboard", "Wedding");
                }
            return View ("Login"); // should be some redirection here
        }
        [HttpGet("logout")]
        public IActionResult LogOut(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        
        
    }
}
