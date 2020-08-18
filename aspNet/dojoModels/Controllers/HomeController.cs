using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dojoModels.Models;

namespace dojoModels.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string Message = "String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message String Message ";
            return View("Index", Message);
        }

        [HttpGet]
        [Route("numbers")]
        public IActionResult Numbers()
        {
            int [] numbers ={1,2,3,10,43,5};

            return View("Numbers", numbers);
        }

        // [HttpGet]
        // [Route("users")]
        // public IActionResult Users()
        // {
        //     List<User> users = new List <User>()
        //     {
        //         new User ("Moose", "Phillips"),
        //         new User ("Sarah", "Gale"),
        //         new User ("Sasha", "Vin"),
        //         new User ("Kostya", "Dzu")
        //     };
        //     return View("Users", User);
        // }

        [HttpGet]
        [Route("user")]
        public IActionResult User()
        {
            User newUser = new User(){
                FirstName = "Sasha",
                LastName = "Vin"
            };
            return View("User", newUser);
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
