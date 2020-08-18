using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace razor.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<string> food = new List<string> {"BBQ Ribs", "Fried Chicken", "Fish and Chips", "Pasta", "Lasagna"};
            ViewBag.delishfood =food; 
            return View();
        }

        [HttpGet]
        [Route("time")]
        public IActionResult Time()
        {
            var dat1 = DateTime.Now.ToString("dddd, MMM dd yyyy, hh:mm:ss");
            ViewBag.time = dat1; 
            return View();
        }
    }
}