using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Personal.Portfolio.Models;

namespace Personal.Portfolio.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.subtitle = "Developer with a passion for";
            ViewBag.title = "ASP.NET MVC";
            
            return View();
        }
        
        [HttpGet]
        public IActionResult Portfolio()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Project(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Resume()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Blog()
        {
            return View();
        }
        
        
        [HttpGet]
        public IActionResult Contact()
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
