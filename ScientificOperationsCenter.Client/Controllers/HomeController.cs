using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ScientificOperationsCenter.Controllers
{
    public sealed class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to the Scientific Operations Center";
            Log.Information("Serving: HomeController -> Index()");
            return View();
        }
    }
}