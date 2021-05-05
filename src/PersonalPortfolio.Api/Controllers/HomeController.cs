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
            SetSeoInformation(
                localizer["Technology<br/>Architect."],
                localizer["Hi, I'm Victor. I am the author of this blog, I share knowledge and work as a consultant in an outsourcing company"]
            );

            var model = new IndexViewModel();

            var projects = await projectRepository.GetAsync();
            model.Projects = projects.Select(project =>
            {
                return new ProjectViewModel
                {
                    Id = project.ProjectId,
                    Description = project.Description,
                    Showcase = imageHelper.getFilePath(project.ShowcaseImage),
                    Title = project.Title,
                    Url = project.Url
                };
            })?.ToList();

            var posts = await postRepository.GetAsync();
            model.Posts = posts.Select(post =>
            {
                return new PostViewModel
                {
                    Id = post.PostId,
                    Title = post.Title,
                    ShowcaseImage = imageHelper.getFilePath(post.ShowcaseImage),
                    CreatedAt = post.CreatedAt,
                    Category = post.Category.Name
                };
            })?.ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Portfolio()
        {
            SetSeoInformation(
                localizer["Tech Arch<br/>Projects"],
                localizer["Here you'll find my portfolio work and what I've accomplished over the years as a Front-End Developer"]
            );

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
                });
            }

            return View(model);
        }

        [HttpGet]
        [Route("home/portfolio/{id}")]
        public async Task<IActionResult> Project(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return View();
            }

            ViewBag.darkHeader = "dark-header";

            var project = await projectRepository.GetByTitleAsync(id.RemoveDash());
            var model = new ProjectViewModel
            {
                Description = project.Description,
                Id = project.ProjectId,
                Showcase = imageHelper.getFilePath(project.ShowcaseImage),
                Title = project.Title,
                Url = project.Url,
                Technologies = string.Join(",", project.Technologies.Select(a => a.Name))
            };

            SetSeoInformation(string.Empty, project.ShortDescription);

            return View(model);
        }

        [HttpGet]
        public IActionResult About()
        {
            SetSeoInformation(
                localizer["My passions &<br/>Personality"],
                localizer["Take a look into my life, and learn how I became a Developer."]
            );

            return View();
        }

        [HttpGet]
        public IActionResult Resume()
        {
            return View();
        }

        [HttpGet]
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
        [Route("home/blog/category/{id}")]
        public async Task<IActionResult> Category(string id)
        {
            ViewBag.darkHeader = "dark-header";

            var model = new List<PostViewModel>();
            var posts = await postRepository.GetByCategoryAsync(id.RemoveDash());
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
        public async Task<IActionResult> Post(string id)
        {
            ViewBag.darkHeader = "dark-header";
            if (string.IsNullOrWhiteSpace(id))
            {
                return View();
            }

            var post = await postRepository.GetByTitleAsync(id.RemoveDash());
            var model = new PostViewModel
            {
                Id = post.PostId,
                Title = post.Title,
                Description = post.Description,
                ShowcaseImage = imageHelper.getFilePath(post.ShowcaseImage),
                CreatedAt = post.CreatedAt,
                Category = post.Category?.Name
            };

            SetSeoInformation(string.Empty, post.ShortDescription);

            return View(model);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            SetSeoInformation(
                localizer["Let's have a chat"],
                localizer["Contact me today if you're interested in hiring a true Tecnology Expert with years of experience as a Developer"]
            );

            return View();
        }

        private void SetSeoInformation(string title, string description)
        {
            ViewBag.title = title;
            ViewBag.Description = description;
        }
    }
}
