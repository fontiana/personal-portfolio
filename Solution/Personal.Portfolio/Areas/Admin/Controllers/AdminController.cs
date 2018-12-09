using Microsoft.AspNetCore.Mvc;

namespace Personal.Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {            
            return View();
        }
    }
}
