using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace dojoSurvey.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("result")]
        public IActionResult Result(string Name, string Location, string Language, string Comment)
        {
            ViewBag.name = Name;
            ViewBag.location = Location;
            ViewBag.language = Language;
            ViewBag.comment = Comment;
            return View();
        }
    }
}