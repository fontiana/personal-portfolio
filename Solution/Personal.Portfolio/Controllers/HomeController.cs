using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Personal.Portfolio.Models;

namespace Personal.Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
    
        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            this.SetBanner(localizer["Let's build something amazing together"], localizer["I'm here to create meaningful and lasting relationships with my clients."]);
            return View();
        }

        [HttpGet]
        public IActionResult Portfolio()
        {
            this.SetBanner(localizer["My projects"], localizer["My work as a Full-stack developer."]);
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
