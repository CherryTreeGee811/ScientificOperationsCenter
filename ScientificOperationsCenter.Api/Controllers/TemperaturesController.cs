using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Serilog;


namespace ScientificOperationsCenter.Api.Controllers
{
    /// <summary>
    /// Controller for handling temperature API requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class TemperaturesController : ControllerBase
    {
        private readonly ITemperaturesMapper _temperaturesMapper;


        /// <summary>
        /// Initializes a new instance of the <see cref="TemperaturesController"/> class.
        /// </summary>
        /// <param name="temperaturesMapper">Mapper for temperature data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="temperaturesMapper"/> is null.</exception>
        public TemperaturesController(ITemperaturesMapper temperaturesMapper)
        {
            _temperaturesMapper = temperaturesMapper ?? throw new ArgumentNullException(nameof(temperaturesMapper));
        }


        /// <summary>
        ///  Retrieves hourly temperature averages for a specific day.
        /// </summary>
        /// <param name="date">User provided date for which to retrieve temperature averages, expecting valid date in YYYY-MM-DD format.</param>
        /// <returns>Temperatures for the specified day, ordered by hour.</returns>
        [HttpGet("day")]
        public async Task<IActionResult> Day([FromQuery] string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }
            try
            {
                if (DateOnly.TryParse(date, out DateOnly dateOnly) && dateOnly != DateOnly.MinValue)
                {
                    var temperatures = await _temperaturesMapper.GetTemperaturesForTheDayAsync(dateOnly);
                    if (temperatures.Count() > 0)
                    {
                        Log.Information("Serving: TemperaturesController -> Day()");
                        return Ok(temperatures);
                    }
                    return NoContent();
                }
                return BadRequest("Invalid date format.");
            }
            catch (Exception)
            {
                Log.Error("TemperaturesController -> Day() -> Returned status code 500.");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Retrieves daily temperature averages for a specific month.
        /// </summary>
        /// <param name="date">User provided date for which to retrieve temperature averages, expecting valid date in YYYY-MM-DD format.</param>
        /// <returns>Temperatures for the specified month, ordered by day.</returns>
        [HttpGet("month")]
        public async Task<IActionResult> Month([FromQuery] string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }
            try
            {
                if (DateOnly.TryParse(date, out DateOnly dateOnly) && dateOnly != DateOnly.MinValue)
                {
                    var temperatures = await _temperaturesMapper.GetTemperaturesForTheMonthAsync(dateOnly);
                    if (temperatures.Count() > 0)
                    {
                        Log.Information("Serving: TemperaturesController -> Month()");
                        return Ok(temperatures);
                    }
                    return NoContent();
                }
                return BadRequest("Invalid date format.");
            }
            catch (Exception)
            {
                Log.Error("TemperaturesController -> Month() -> Returned status code 500.");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Retrieves monthly temperature averages for a specific year.
        /// </summary>
        /// <param name="date">User provided date for which to retrieve temperature averages, expecting valid date in YYYY-MM-DD format.</param>
        /// <returns>Temperatures for the specified year, ordered by month.</returns>
        [HttpGet("year")]
        public async Task<IActionResult> Year([FromQuery] string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }
            try
            {
                if (DateOnly.TryParse(date, out DateOnly dateOnly) && dateOnly != DateOnly.MinValue)
                {
                    var temperatures = await _temperaturesMapper.GetTemperaturesForTheYearAsync(dateOnly);
                    if (temperatures.Count() > 0)
                    {
                        Log.Information("Serving: TemperaturesController -> Year()");
                        return Ok(temperatures);
                    }
                    return NoContent();
                }
                return BadRequest("Invalid date format.");
            }
            catch (Exception)
            {
                Log.Error("TemperaturesController -> Year() -> Returned status code 500.");
                return StatusCode(500);
            }
        }
    }
}
