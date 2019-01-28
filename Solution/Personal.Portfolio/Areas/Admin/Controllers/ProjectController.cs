using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Personal.Portfolio.Areas.Admin.Models;

namespace Personal.Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {     
        [HttpGet]   
        public IActionResult Index()
        {
            var model = new List<ProjectViewModel>();
            model.Add(new ProjectViewModel { Description = "Jonas", Title = "Brothers" });
            model.Add(new ProjectViewModel { Description = "Jonas", Title = "Brothers" });
            model.Add(new ProjectViewModel { Description = "Jonas", Title = "Brothers" });
            model.Add(new ProjectViewModel { Description = "Jonas", Title = "Brothers" });
        
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProjectViewModel model)
        {
            model.Add();

            return View();
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
