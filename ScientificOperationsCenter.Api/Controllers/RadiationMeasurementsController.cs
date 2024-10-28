using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Serilog;


namespace ScientificOperationsCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RadiationMeasurementsController : ControllerBase
    {
        private readonly IRadiationMeasurementsMapper _radiationMeasurementsMapper;


        public RadiationMeasurementsController(IRadiationMeasurementsMapper radiationMeasurementsMapper)
        {
            _radiationMeasurementsMapper = radiationMeasurementsMapper;
        }


        [HttpGet("day")]
        public async Task<IActionResult> Day([FromQuery] string date)
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
            return BadRequest("Date parameter is required.");
        }


        [HttpGet("month")]
        public async Task<IActionResult> Month([FromQuery] string date)
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
            return BadRequest("Date parameter is required.");
        }


        [HttpGet("year")]
        public async Task<IActionResult> Year([FromQuery] string date)
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
            return BadRequest("Date parameter is required.");
        }
    }
}