using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(ErrorViewModel model)
        {
            return View(model);
        }

        public IActionResult NotFound(ErrorViewModel model)
        {
            return View(model);
        }
    }
}
