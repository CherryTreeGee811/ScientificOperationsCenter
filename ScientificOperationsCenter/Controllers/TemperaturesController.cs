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


        [HttpGet("/[controller]/Day/{date}")]
        public IActionResult Day(DateOnly date)
        {
            var temperatures = _temperaturesMapper.GetTemperaturesForTheDay(date);
            return View(temperatures);
        }


        [HttpGet("/[controller]/Month/{date}")]
        public IActionResult Month(DateOnly date)
        {
            var temperatures = _temperaturesMapper.GetTemperaturesForTheMonth(date);
            return View(temperatures);
        }


        [HttpGet("/[controller]/Year/{date}")]
        public IActionResult Year(DateOnly date)
        {
            var temperatures = _temperaturesMapper.GetTemperaturesForTheYear(date);
            return View(temperatures);
        }
    }
}
