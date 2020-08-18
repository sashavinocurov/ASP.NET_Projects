using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Login.Models;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context){
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User newUser){
            if(ModelState.IsValid){
                if(dbContext.Users.Any(u => u.Email == newUser.Email)){
                    ModelState.AddModelError("Email", "This Email already exists!");
                    return View("Index");
                }
                PasswordHasher <User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                User lastUser = dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
                HttpContext.Session.SetInt32("userid", lastUser.UserId);
                return RedirectToAction("Success");
            }
            else{
                return View("Index");
            }
        }

        [HttpGet("loginpage")]
        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser userSubmit)
        {
            if(ModelState.IsValid)
            {
                var UserInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmit.Email);
                if(UserInDb == null){
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmit, UserInDb.Password, userSubmit.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Incorrect Password");
                    return View("Login");
                }
                User loggedUser = dbContext.Users.FirstOrDefault(u => u.Email == userSubmit.Email);
                HttpContext.Session.SetInt32("userid", loggedUser.UserId);
                return RedirectToAction("Success");
            }
            return View("Login");
        }
        [HttpGet("success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetInt32("userid") == null)
            {
                return RedirectToAction("Index");
            }
            int userIdsession = (int)HttpContext.Session.GetInt32("userid");
            User loggedUser = dbContext.Users.FirstOrDefault(u => u.UserId == userIdsession);
            return View();
        }
        [HttpGet("logout")]
        public IActionResult LogOut(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
