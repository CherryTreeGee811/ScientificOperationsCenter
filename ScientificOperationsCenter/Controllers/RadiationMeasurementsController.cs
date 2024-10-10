using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Mappers.Interfaces;

namespace ScientificOperationsCenter.Controllers
{
    public class RadiationMeasurementsController : Controller
    {
        private IRadiationMeasurementsMapper _radiationMeasurementsMapper;


        public RadiationMeasurementsController(IRadiationMeasurementsMapper radiationMeasurementsMapper)
        {
            _radiationMeasurementsMapper = radiationMeasurementsMapper;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("/[controller]/Day/{date}")]
        public IActionResult Day(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(date);
            return View(radiationMeasurements);
        }


        [HttpGet("/[controller]/Month/{date}")]
        public IActionResult Month(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(date);
            return View(radiationMeasurements);
        }


        [HttpGet("/[controller]/Year/{date}")]
        public IActionResult Year(DateOnly date)
        {
            var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(date);
            return View(radiationMeasurements);
        }
    }
}
