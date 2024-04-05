using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
