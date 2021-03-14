using System.Collections.Generic;
using PersonalPortfolio.Helper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Areas.Admin.Models;
using PersonalPortfolio.Context.Entity;
using PersonalPortfolio.Repository.Project;
using System;

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

            await model.Image.SaveImageAsync();
            await projectRepository.AddAsync(new ProjectEntity
            {
                Title = model.Title,
                Description = model.Description,
                Url = model.Url,
                CreatedAt = DateTime.Now,
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

            var project = await projectRepository.GetByIdAsync(id.Value);
            var model = new ProjectViewModel
            {
                Id = project.ProjectId,
                Description = project.Description,
                Title = project.Title,
                Url = project.Url,
                TechStack = string.Join(", ", project.Technologies?.Select(p => p.Name)),
                //Image = project.ShowcaseImage.LoadImage(),
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

            await model.Image.SaveImageAsync();
            var project = await projectRepository.GetByIdAsync(model.Id);
            
            project.Title = model.Title;
            project.Description = model.Description;
            project.ShowcaseImage = model.Image.FileName;
            project.Technologies = model.TechStack?.Split(',').Select(tech => new TechnologyEntity { Name = tech }).ToList();

            projectRepository.Update(project);
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
