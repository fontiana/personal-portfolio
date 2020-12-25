using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Areas.Admin.Models;
using PersonalPortfolio.Context.Entity;
using PersonalPortfolio.Repository.Project;

namespace PersonalPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
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

            var filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await model.Image.CopyToAsync(stream);
            }

            await projectRepository.AddAsync(new ProjectEntity
            {
                Title = model.Title,
                Description = model.Description,
                Technologies = model.TechStack?.Split(',').Select(tech => new TechnologyEntity { Name = tech }).ToList(),
                ShowcaseImage = model.Image.FileName
            });
            await projectRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var project = await projectRepository.GetByIDAsync(id.Value);
            var model = new ProjectViewModel
            {
                Id = project.ProjectId,
                Description = project.Description,
                Title = project.Title,
                TechStack = string.Join(", ", project.Technologies?.Select(p => p.Name))
                //Image = project.ShowcaseImage
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            projectRepository.Update(new ProjectEntity
            {
                Title = model.Title,
                Description = model.Description,
                ShowcaseImage = model.Image.FileName
            });
            await projectRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                await projectRepository.DeleteAsync(id.Value);
                await projectRepository.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
