using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Areas.Admin.Models;
using PersonalPortfolio.Context.Entity;
using PersonalPortfolio.Helper;
using PersonalPortfolio.Repository.Post;

namespace PersonalPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IImageHelper imageHelper;

        public BlogController(IPostRepository postRepository, IImageHelper imageHelper)
        {
            this.postRepository = postRepository;
            this.imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            var model = new List<PostViewModel>();
            var posts = await postRepository.GetAsync();

            foreach (var item in posts)
            {
                model.Add(new PostViewModel
                {
                    Description = item.Description,
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
        public async Task<IActionResult> Add(PostViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            { 
                return View("Add", model);
            }

            var showcaseImageName = await imageHelper.saveFileAsync(model.Image);
            await postRepository.AddAsync(new PostEntity
            {
                Title = model.Title,
                Description = model.Description,
                ShortDescription = model.ShortDescription,
                Category = new CategoryEntity {  Name = model.Category },
                ShowcaseImage = showcaseImageName,
                CreatedAt = DateTime.Now
            });
            await postRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var post = await postRepository.GetByIdAsync(id.Value);
            var model = new PostViewModel
            {
                Id = post.PostId,
                Description = post.Description,
                ShortDescription = post.ShortDescription,
                Title = post.Title,
                Category = post.Category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var showcaseImageName = await imageHelper.saveFileAsync(model.Image);
            var post = await postRepository.GetByIdAsync(model.Id);
            post.Category.Name = model.Category;
            post.Title = model.Title;
            post.Description = model.Description;
            post.ShortDescription = model.ShortDescription;
            post.Category = post.Category;
            post.ShowcaseImage = showcaseImageName;

            postRepository.Update(post);
            await postRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                await postRepository.DeleteAsync(id.Value);
                await postRepository.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
