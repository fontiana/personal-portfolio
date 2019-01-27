using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectViewModel model)
        {
            return View();
        }
        
        [HttpGet]
        //[HttpPost("{categoryId}")]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
