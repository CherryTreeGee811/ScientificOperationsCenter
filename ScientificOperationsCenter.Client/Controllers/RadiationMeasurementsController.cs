using Microsoft.AspNetCore.Mvc;
using System;


namespace ScientificOperationsCenter.Controllers
{
    public sealed class RadiationMeasurementsController : Controller
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
