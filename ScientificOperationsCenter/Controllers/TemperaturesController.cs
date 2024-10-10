using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Mappers.Interfaces;


namespace ScientificOperationsCenter.Controllers
{
    public class TemperaturesController : Controller
    {
        private ITemperaturesMapper _temperaturesMapper;


        public TemperaturesController(ITemperaturesMapper temperaturesMapper)
        {
            _temperaturesMapper = temperaturesMapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("/[controller]/Day")]
        public IActionResult Day([FromQuery] DateOnly? date)
        {
            if (date.HasValue)
            {
                var temperatures = _temperaturesMapper.GetTemperaturesForTheDay((DateOnly)date);
                return Json(temperatures);
            }
            return View();
        }


        [HttpGet("/[controller]/Month")]
        public IActionResult Month([FromQuery] DateOnly? date)
        {
            if (date.HasValue)
            {
                var temperatures = _temperaturesMapper.GetTemperaturesForTheMonth((DateOnly)date);
                return Json(temperatures);
            }
            return View();
        }


        [HttpGet("/[controller]/Year")]
        public IActionResult Year([FromQuery] DateOnly? date)
        {
            if (date.HasValue)
            {
                var temperatures = _temperaturesMapper.GetTemperaturesForTheYear((DateOnly)date);
                return Json(temperatures);
            }
            return View();
        }
    }
}
