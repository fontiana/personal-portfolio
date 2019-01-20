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
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(ProjectViewModel model)
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Edit(ProjectViewModel model)
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
