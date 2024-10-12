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
                try
                {
                    var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay((DateOnly)date);
                    if (radiationMeasurements.Count() > 0)
                    {
                        return Ok(radiationMeasurements);
                    }
                    return NotFound();
                } 
                catch (Exception)
                {
                    return StatusCode(500); 
                }
            }
            return View();
        }


        [HttpGet("/[controller]/Month")]
        public IActionResult Month([FromQuery] DateOnly? date)
        {
            if (date.HasValue)
            {
                var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth((DateOnly)date);
                if (radiationMeasurements.Count() > 0)
                {
                    return Ok(radiationMeasurements);
                }
                return NotFound();
            }
            return View();
        }


        [HttpGet("/[controller]/Year")]
        public IActionResult Year([FromQuery] DateOnly? date)
        {
            if (date.HasValue)
            {
                var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear((DateOnly)date);
                if (radiationMeasurements.Count() > 0)
                {
                    return Json(radiationMeasurements);
                }
                return NotFound();
            }
            return View();
        }
    }
}
