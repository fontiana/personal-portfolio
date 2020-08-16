using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Areas.Admin.Models;
using PersonalPortfolio.Context;

namespace PersonalPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly PortfolioContext context;

        public BlogController(PortfolioContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new List<PostViewModel>();
            var posts = await context.Posts.ToListAsync();

            foreach (var item in posts)
            {
                model.Add(new PostViewModel
                {
                    Description = item.Descriptiong,
                    Title = item.Title,
                    Id = item.PostId,
                });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View("Add", model);
            }

            await context.Posts.AddAsync(new Post
            {
                Title = model.Title,
                Descriptiong = model.Description
            });
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var post = await context.Posts.FindAsync(id.Value);
            var model = new PostViewModel
            {
                Id = post.PostId,
                Description = post.Descriptiong,
                Title = post.Title
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            context.Posts.Update(new Post
            {
                Title = model.Title,
                Descriptiong = model.Description,
            });
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var post = await context.Posts.FindAsync(id.Value);
                if (post != null)
                {
                    context.Posts.Remove(post);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
