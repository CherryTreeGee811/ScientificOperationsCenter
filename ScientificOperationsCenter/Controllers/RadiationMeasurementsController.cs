using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScientificOperationsCenter.Mappers.Interfaces;


namespace ScientificOperationsCenter.Controllers
{
    public sealed class RadiationMeasurementsController : Controller
    {
        private readonly IRadiationMeasurementsMapper _radiationMeasurementsMapper;


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
        public IActionResult Day([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDay(dateOnly);
                        if (radiationMeasurements.Count() > 0)
                        {
                            return Ok(radiationMeasurements);
                        }
                        return NotFound();
                    }
                    return BadRequest();
                } 
                catch (Exception)
                {
                    return StatusCode(500); 
                }
            }
            return View();
        }


        [HttpGet("/[controller]/Month")]
        public IActionResult Month([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonth(dateOnly);
                        if (radiationMeasurements.Count() > 0)
                        {
                            return Ok(radiationMeasurements);
                        }
                        return NotFound();
                    }
                    return BadRequest();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            return View();
        }


        [HttpGet("/[controller]/Year")]
        public IActionResult Year([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYear(dateOnly);
                        if (radiationMeasurements.Count() > 0)
                        {
                            return Ok(radiationMeasurements);
                        }
                        return NotFound();
                    }
                    return BadRequest();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            return View();
        }
    }
}
