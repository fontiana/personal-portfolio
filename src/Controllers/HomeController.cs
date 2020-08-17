using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using PersonalPortfolio.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Repository.Project;

namespace PersonalPortfolio.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly IProjectRepository projectRepository;

        public HomeController(IStringLocalizer<HomeController> localizer, IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SetBanner(localizer["Technology<br/>Architect."]);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Portfolio()
        {
            SetBanner(localizer["Tech Arch<br/>Projects"]);

            var projects = await projectRepository.GetAsync();

            foreach (var item in projects)
            {
                //model.Add(new ProjectViewModel
                //{
                //    Description = item.Description,
                //    Title = item.Title,
                //    Id = item.ProjectId,
                //});
            }

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
