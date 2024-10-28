using Microsoft.AspNetCore.Mvc;


namespace ScientificOperationsCenter.Controllers
{
    public sealed class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to the Scientific Operations Center";
            return View();
        }
    }
}
