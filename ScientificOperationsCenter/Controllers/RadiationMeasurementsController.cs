using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Mappers.Interfaces;
using Serilog;


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
            Log.Information("Serving: RadiationMeasurementsController -> Index()");
            return View();
        }


        [HttpGet("/[controller]/Day")]
        public async Task<IActionResult> Day([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(dateOnly);
                        if (radiationMeasurements.Count() > 0)
                        {
                            Log.Information("Serving: RadiationMeasurementsController -> Day()");
                            return Ok(radiationMeasurements);
                        }
                        return NotFound();
                    }
                    return BadRequest();
                } 
                catch (Exception)
                {
                    Log.Error("RadiationMeasurementsController -> Day() -> Returned status code 500.");
                    return StatusCode(500); 
                }
            }
            return View();
        }


        [HttpGet("/[controller]/Month")]
        public async Task<IActionResult> Month([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(dateOnly);
                        if (radiationMeasurements.Count() > 0)
                        {
                            Log.Information("Serving: RadiationMeasurementsController -> Month()");
                            return Ok(radiationMeasurements);
                        }
                        return NotFound();
                    }
                    return BadRequest();
                }
                catch (Exception)
                {
                    Log.Error("RadiationMeasurementsController -> Month() -> Returned status code 500.");
                    return StatusCode(500);
                }
            }
            return View();
        }


        [HttpGet("/[controller]/Year")]
        public async Task<IActionResult> Year([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(dateOnly);
                        if (radiationMeasurements.Count() > 0)
                        {
                            Log.Information("Serving: RadiationMeasurementsController -> Year()");
                            return Ok(radiationMeasurements);
                        }
                        return NotFound();
                    }
                    return BadRequest();
                }
                catch (Exception)
                {
                    Log.Error("RadiationMeasurementsController -> Year() -> Returned status code 500.");
                    return StatusCode(500);
                }
            }
            return View();
        }
    }
}