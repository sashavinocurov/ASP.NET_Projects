using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace welcomeToASPNET.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<string> someThings = new List<string> {"Hello", "Goodbye", "Puglishes", "Ok"};
            ViewBag.whatevername = someThings;
            ViewBag.arandomnumber = 152;
            ViewBag.isPuglishes = true;
            return View();
        }

        [HttpGet]
        [Route("otherpage")]
        public IActionResult OtherPage()
        {
            return View();
        }
    }
}