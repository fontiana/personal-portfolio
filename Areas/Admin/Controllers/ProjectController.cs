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
    public class ProjectController : Controller
    {
        private readonly PortfolioContext context;

        public ProjectController(PortfolioContext context)
        {
            this.context = context;
        }

        [HttpGet]   
        public async Task<IActionResult> Index()
        {
            var model = new List<ProjectViewModel>();
            var projects = await context.Projects.ToListAsync();

            foreach (var item in projects)
            {
                model.Add(new ProjectViewModel {
                    Description = item.Description,
                    Title = item.Title,
                    Id = item.ProjectId,
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
        public async Task<IActionResult> Add(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View("Add", model);
            }

            await context.Projects.AddAsync(new Project
            {
                Title = model.Title,
                Description = model.Description,
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

            var model = new ProjectViewModel();
            model.GetById(id.Value);
            return View(model);
        }
        
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectViewModel model)
        {
            model.Edit();

            return View();
        }
        
        [HttpGet]
        //[HttpPost("{categoryId}")]
        public IActionResult Delete(int? id)
        {
            return View();
        }
    }
}
