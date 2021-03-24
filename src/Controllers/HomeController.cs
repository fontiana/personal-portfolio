using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PersonalPortfolio.Models;
using PersonalPortfolio.Repository.Post;
using PersonalPortfolio.Repository.Project;
using PersonalPortfolio.Helper;

namespace PersonalPortfolio.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly IProjectRepository projectRepository;
        private readonly IPostRepository postRepository;
        private readonly IImageHelper imageHelper;

        public HomeController(

            IStringLocalizer<HomeController> localizer,
            IProjectRepository projectRepository,
            IPostRepository postRepository,
            IImageHelper imageHelper)
        {
            this.projectRepository = projectRepository;
            this.postRepository = postRepository;
            this.localizer = localizer;
            this.imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            SetBanner(localizer["Technology<br/>Architect."]);
            var model = new IndexViewModel();
            //model.Projects = await projectRepository.GetAsync();
            //model.Posts = await postRepository.GetAsync();

            return View(model);
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
                    Showcase = imageHelper.getFilePath(item.ShowcaseImage)
                }); ;
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
                Showcase = imageHelper.getFilePath(project.ShowcaseImage),
                Title = project.Title,
                Url = project.Url,
                Technologies = string.Join(",", project.Technologies.Select(a => a.Name))
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
                    Id = item.PostId,
                    ShowcaseImage = imageHelper.getFilePath(item.ShowcaseImage),
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
                ShowcaseImage = imageHelper.getFilePath(post.ShowcaseImage),
                CreatedAt = post.CreatedAt,
                Category = post.Category?.Name
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
