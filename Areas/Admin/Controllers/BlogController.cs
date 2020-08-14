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
        public IActionResult Add(PostViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View("Add", model);
            }

            await context.Posts.AddAsync(new Post
            {
                Title = model.Title,
                Description = model.Description
            });
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var model = new PostViewModel();
            //model.GetById(id.Value);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View();
        }
    }
}
