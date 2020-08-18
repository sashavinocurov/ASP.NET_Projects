using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sessionPractice.Models;
using Microsoft.AspNetCore.Http;

namespace sessionPractice.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int? counter = HttpContext.Session.GetInt32("counter");
            if(counter != null)
            {
                HttpContext.Session.SetInt32("counter", (int)counter+1);
                counter ++;
            } else{
                HttpContext.Session.SetInt32("counter", 0);
            }

            ViewBag.MyCounter = HttpContext.Session.GetInt32("counter");
            ViewBag.MyMantra = HttpContext.Session.GetString("thisismykey");
            return View();
        }

        [HttpPost("newmantra")]
        public RedirectToActionResult NewMantra(string mantra)
        {
            HttpContext.Session.SetString("thisismykey", mantra);
            return RedirectToAction("Index");
        }
        [HttpGet("clearsession")]
        public RedirectToActionResult ClearSession()
        {
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
