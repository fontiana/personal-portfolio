﻿using System.Collections.Generic;
using PersonalPortfolio.Helper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Areas.Admin.Models;
using PersonalPortfolio.Context.Entity;
using PersonalPortfolio.Repository.Project;
using System;
using System.Threading;

namespace PersonalPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;
        private readonly IImageHelper imageHelper;

        public ProjectController(IProjectRepository projectRepository, IImageHelper imageHelper)
        {
            this.projectRepository = projectRepository;
            this.imageHelper = imageHelper;

        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var model = new List<ProjectViewModel>();
            var projects = await projectRepository.GetAsync(cancellationToken);

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
        public async Task<IActionResult> Add(ProjectViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            var showcaseImageName = await imageHelper.saveFileAsync(model.Image);
            await projectRepository.AddAsync(new ProjectEntity
            {
                Title = model.Title,
                Description = model.Description,
                ShortDescription = model.ShortDescription,
                Url = model.Url,
                CreatedAt = DateTime.Now,
                Technologies = model.TechStack?.Split(',').Select(tech => new TechnologyEntity { Name = tech }).ToList(),
                ShowcaseImage = showcaseImageName
            }, cancellationToken);
            await projectRepository.Save(cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var project = await projectRepository.GetByIdAsync(id.Value, cancellationToken);
            var model = new ProjectViewModel
            {
                Id = project.ProjectId,
                Description = project.Description,
                ShortDescription = project.ShortDescription,
                Title = project.Title,
                Url = project.Url,
                TechStack = string.Join(", ", project.Technologies?.Select(p => p.Name)),
                //Image = project.ShowcaseImage.LoadImage(),
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            var showcaseImageName = await imageHelper.saveFileAsync(model.Image);
            var project = await projectRepository.GetByIdAsync(model.Id, cancellationToken);
            
            project.Title = model.Title;
            project.Description = model.Description;
            project.ShowcaseImage = showcaseImageName;
            project.ShortDescription = model.ShortDescription;
            project.Url = model.Url;
            project.Technologies = model.TechStack?.Split(',').Select(tech => new TechnologyEntity { Name = tech }).ToList();

            projectRepository.Update(project);
            await projectRepository.Save(cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                await projectRepository.DeleteAsync(id.Value, cancellationToken);
                await projectRepository.Save(cancellationToken);
            }

            return RedirectToAction("Index");
        }
    }
}
