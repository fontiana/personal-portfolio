using System;
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
            this.SetBanner("ASP.NET MVC", "Developer with a passion for");
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

        private void SetBanner(string title, string subtitle)
        {
            ViewBag.subtitle = subtitle;
            ViewBag.title = title;
        }
    }
}
