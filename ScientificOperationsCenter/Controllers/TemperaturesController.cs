using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Day([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var temperatures = _temperaturesMapper.GetTemperaturesForTheDay(dateOnly);
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
        public IActionResult Month([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var temperatures = _temperaturesMapper.GetTemperaturesForTheMonth(dateOnly);
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
        public IActionResult Year([FromQuery] string? date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                try
                {
                    var success = DateOnly.TryParse(date, out DateOnly dateOnly);
                    if (success)
                    {
                        var temperatures = _temperaturesMapper.GetTemperaturesForTheYear(dateOnly);
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
