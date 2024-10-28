using Microsoft.AspNetCore.Mvc;


namespace ScientificOperationsCenter.Client.Controllers
{
    public sealed class TemperaturesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Day()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Month()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Year()
        {
            return View();
        }
    }
}
