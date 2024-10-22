using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScientificOperationsCenter.Mappers.Interfaces;


namespace ScientificOperationsCenter.Controllers
{
    public sealed class TemperaturesController : Controller
    {
        private readonly ITemperaturesMapper _temperaturesMapper;


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
        public async Task<IActionResult> Day([FromQuery] string? date)
        {
            if (!date.IsNullOrEmpty())
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
            return View();
        }
    }
}
