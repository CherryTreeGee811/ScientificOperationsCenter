using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Mappers.Interfaces;


namespace ScientificOperationsCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class TemperaturesController : ControllerBase
    {
        private readonly ITemperaturesMapper _temperaturesMapper;


        public TemperaturesController(ITemperaturesMapper temperaturesMapper)
        {
            _temperaturesMapper = temperaturesMapper;
        }


        [HttpGet("day")]
        public async Task<IActionResult> Day([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var temperatures = await _temperaturesMapper.GetTemperaturesForTheDayAsync(dateOnly);
                        if (temperatures.Count() > 0)
                        {
                            return Ok(temperatures);
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
                        var temperatures = await _temperaturesMapper.GetTemperaturesForTheMonthAsync(dateOnly);
                        if (temperatures.Count() > 0)
                        {
                            return Ok(temperatures);
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
                        var temperatures = await _temperaturesMapper.GetTemperaturesForTheYearAsync(dateOnly);
                        if (temperatures.Count() > 0)
                        {
                            return Ok(temperatures);
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
            return BadRequest("Date parameter is required.");
        }
    }
}
