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
        public async Task<IActionResult> Day([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(dateOnly);
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
        public async Task<IActionResult> Month([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(dateOnly);
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
        public async Task<IActionResult> Year([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(dateOnly);
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
