using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Personal.Portfolio.Models;

namespace Personal.Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.subtitle = "Developer with a passion for";
            ViewBag.title = "ASP.NET MVC";
            
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

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
