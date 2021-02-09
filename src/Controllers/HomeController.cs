using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PersonalPortfolio.Models;
using PersonalPortfolio.Repository.Post;
using PersonalPortfolio.Repository.Project;

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
        public async Task<IActionResult> Project(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return View();
            }

            ViewBag.darkHeader = "dark-header";

            var project = await projectRepository.GetByTitleAsync(id);
            var model = new ProjectViewModel
            {
                Description = project.Description,
                Id = project.ProjectId,
                Showcase = project.ShowcaseImage,
                Title = project.Title,
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
        public async Task<IActionResult> Blog()
        {
            ViewBag.darkHeader = "dark-header";

            var model = new List<PostViewModel>();
            var posts = await postRepository.GetAsync();
            foreach (var item in posts)
            {
                model.Add(new PostViewModel
                {
                    Description = item.Description,
                    Title = item.Title,
                    Category = item.Category.Name,
                    Id = item.PostId
                });
            }

            return View(model);
        }

        [HttpGet]
        [Route("home/blog/{id}")]
        public async Task<IActionResult> Post(int? id)
        {
            ViewBag.darkHeader = "dark-header";
            if (!id.HasValue)
            {
                return View();
            }

            var post = await postRepository.GetByIdAsync(id.Value);
            var model = new PostViewModel
            {
                Id = post.PostId,
                Title = post.Title,
                Description = post.Description,
                ShowcaseImage = post.ShowcaseImage
            };

            return View(model);
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
