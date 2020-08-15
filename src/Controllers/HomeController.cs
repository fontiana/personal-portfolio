using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace PersonalPortfolio.Controllers
{
    [AllowAnonymous]
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
            SetBanner(localizer["Technology<br/>Architect."]);
            return View();
        }

        [HttpGet]
        public IActionResult Portfolio()
        {
            SetBanner(localizer["Tech Arch<br/>Projects"]);
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
            SetBanner(localizer["My passions &<br/>Personality"]);
            return View();
        }

        [HttpGet]
        public IActionResult Resume()
        {
            return View();
        }

        [HttpGet]
        [Route("home/blog")]
        public IActionResult Blog()
        {
            ViewBag.darkHeader = "dark-header";
            return View();
        }

        [HttpGet]
        [Route("home/blog/{id}")]
        public IActionResult Blog(string id)
        {
            ViewBag.darkHeader = "dark-header";
            if (string.IsNullOrWhiteSpace(id))
            {
                return View();
            }
            else
            {
                return View("BlogPost");
            }
        }

        [HttpGet]
        public IActionResult Contact()
        {
            SetBanner(localizer["Let's have a chat"]);
            return View();
        }

        private void SetBanner(string title)
        {
            ViewBag.title = title;
        }
    }
}
