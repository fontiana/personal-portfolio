using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using PersonalPortfolio.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Repository.Project;
using PersonalPortfolio.Models;
using System.Collections.Generic;
using PersonalPortfolio.Repository.Post;

namespace PersonalPortfolio.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly IProjectRepository projectRepository;
        private readonly IPostRepository postRepository;

        public HomeController(
            IStringLocalizer<HomeController> localizer,
            IProjectRepository projectRepository,
            IPostRepository postRepository)
        {
            this.projectRepository = projectRepository;
            this.postRepository = postRepository;
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

            var model = new List<ProjectViewModel>();
            var projects = await projectRepository.GetAsync();

            foreach (var item in projects)
            {
                model.Add(new ProjectViewModel
                {
                    Description = item.Description,
                    Title = item.Title,
                    Id = item.ProjectId,
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Project(int? id)
        {
            if (!id.HasValue)
            {
                return View();
            }

            var project = await projectRepository.GetByIDAsync(id.Value);

            var model = new ProjectViewModel
            {

            };

            return View(model);
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

            var model = new List<PostViewModel>();
            model.Add(new PostViewModel
            {
                Description = "Lorem ipsum dolores non fat",
                Title = "Test"
            });
            model.Add(new PostViewModel
            {
                Description = "Lorem ipsum dolores non fat",
                Title = "Test"
            });
            model.Add(new PostViewModel
            {
                Description = "Lorem ipsum dolores non fat",
                Title = "Test"
            });

            return View(model);
        }

        [HttpGet]
        [Route("home/blog/{id}")]
        public async Task<IActionResult> Blog(int? id)
        {
            ViewBag.darkHeader = "dark-header";
            if (!id.HasValue)
            {
                return View();
            }

            var posts = await postRepository.GetByIDAsync(id.Value);
            var model = new PostViewModel
            {
                Id = posts.PostId,
                Title = posts.Title,
                Description = posts.Description,
                //Showcase = posts.ShowcaseImage
            };

            return View("BlogPost", model);
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
