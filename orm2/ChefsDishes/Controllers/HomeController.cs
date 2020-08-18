using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chefs_Dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace Chefs_Dishes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Chef> allChefs = dbContext.chefs
            .Include(chef => chef.CreatedDishes).ToList();
            return View("Index", allChefs);
        }
        private MyContext dbContext;
        public HomeController(MyContext context){
            dbContext = context;
        }

        [HttpGet("dishes")]
        public IActionResult ListOfDishes(){
            List<Dish> allDishes = dbContext.dishs.Include(dish => dish.Creator).ToList();
            return View("Dishes", allDishes);
        }
        [HttpPost("/dishes/new")]
        public IActionResult AddNewDish(Dish newDish){
            dbContext.Add(newDish);
            dbContext.SaveChanges();
            return RedirectToAction ("ListOfDishes");
        }
        [HttpGet("newdish")]
        public IActionResult NewDish(){
            List<Chef> allChefs = dbContext.chefs.ToList();
            ViewBag.chefs = allChefs;
            return View ("NewDishes");
        }
        [HttpGet("newchef")]
        public IActionResult NewChef(){
            return View("NewChef");
        }
        [HttpPost("addchef")]
        public IActionResult AddNewChef(Chef newChef){
            if(!ModelState.IsValid){
                return View("NewChef");
            }
            dbContext.Add(newChef);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
