using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using entityPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using entityPractice.Models;

namespace entityPractice.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet ("")]
        public IActionResult Index()
        {
            IndexViewModel viewMod = new IndexViewModel();
            viewMod.RegisteredUsers = dbContext.Users.ToList();
            return View(viewMod);
        }
        
        [HttpPost ("makething")]
        public IActionResult MakeThing(IndexViewModel fromForm)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(fromForm.UserForm);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            fromForm.RegisteredUsers = dbContext.Users.ToList();
            return View("Index", fromForm);
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
