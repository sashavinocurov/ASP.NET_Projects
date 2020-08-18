using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace portfolio.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet]
        [Route("")]

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("projects")]

        public IActionResult Projects()
        {
            return View();
        }

        [HttpGet]
        [Route("contacts")]

        public IActionResult Contacts()
        {
            return View();
        }
    }
}