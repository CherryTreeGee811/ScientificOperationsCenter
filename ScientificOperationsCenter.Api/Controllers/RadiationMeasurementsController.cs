using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.Mappers.Interfaces;
using Serilog;
using System.Globalization;


namespace ScientificOperationsCenter.Api.Controllers
{
    /// <summary>
    /// Controller for handling radiation measurements API requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class RadiationMeasurementsController : ControllerBase
    {
        private readonly IRadiationMeasurementsMapper _radiationMeasurementsMapper;


        /// <summary>
        /// Initializes a new instance of the <see cref="RadiationMeasurementsController"/> class.
        /// </summary>
        /// <param name="radiationMeasurementsMapper">Mapper for radiation measurements data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="radiationMeasurementsMapper"/> is null.</exception>
        public RadiationMeasurementsController(IRadiationMeasurementsMapper radiationMeasurementsMapper)
        {
            _radiationMeasurementsMapper = radiationMeasurementsMapper ?? throw new ArgumentNullException(nameof(radiationMeasurementsMapper));
        }


        /// <summary>
        /// Retrieves hourly total radiation measurements for a specific day.
        /// </summary>
        /// <param name="date">User provided date for which to retrieve radiation measurement totals, expecting valid date in YYYY-MM-DD format.</param>
        /// <returns>Radiation measurements for the specified day, ordered by hour.</returns>
        [HttpGet("day")]
        public async Task<IActionResult> Day([FromQuery] string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }
            try
            {
                if (DateOnly.TryParse(date, new CultureInfo("en-US"), out DateOnly dateOnly) && dateOnly != DateOnly.MinValue)
                {
                    var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheDayAsync(dateOnly);
                    if (radiationMeasurements.Any())
                    {
                        Log.Information("Serving: RadiationMeasurementsController -> Day()");
                        return Ok(radiationMeasurements);
                    }
                    return NoContent();
                }
                return BadRequest("Invalid date format.");
            } 
            catch (Exception gEx)
            {
                Log.Error(gEx, "RadiationMeasurementsController -> Day() -> Returned status code 500.");
                return StatusCode(500); 
            }
        }


        /// <summary>
        /// Retrieves daily total radiation measurements for a specific month.
        /// </summary>
        /// <param name="date">User provided date for which to retrieve radiation measurement totals, expecting valid date in YYYY-MM-DD format.</param>
        /// <returns>Radiation measurements for the specified month, ordered by day.</returns>
        [HttpGet("month")]
        public async Task<IActionResult> Month([FromQuery] string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }
            try
            {
                if (DateOnly.TryParse(date, new CultureInfo("en-US"), out DateOnly dateOnly) && dateOnly != DateOnly.MinValue)
                {
                    var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheMonthAsync(dateOnly);
                    if (radiationMeasurements.Any())
                    {
                        Log.Information("Serving: RadiationMeasurementsController -> Month()");
                        return Ok(radiationMeasurements);
                    }
                    return NoContent();
                }
                return BadRequest("Invalid date format.");
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "RadiationMeasurementsController -> Month() -> Returned status code 500.");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Retrieves monthly total radiation measurements for a specific year.
        /// </summary>
        /// <param name="date">User provided date for which to retrieve radiation measurement totals, expecting valid date in YYYY-MM-DD format.</param>
        /// <returns>Radiation measurements for the specified year, ordered by month.</returns>
        [HttpGet("year")]
        public async Task<IActionResult> Year([FromQuery] string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is required.");
            }
            try
            {
                if (DateOnly.TryParse(date, new CultureInfo("en-US"), out DateOnly dateOnly) && dateOnly != DateOnly.MinValue)
                {
                    var radiationMeasurements = await _radiationMeasurementsMapper.GetRadiationMeasurementsForTheYearAsync(dateOnly);
                    if (radiationMeasurements.Any())
                    {
                        Log.Information("Serving: RadiationMeasurementsController -> Year()");
                        return Ok(radiationMeasurements);
                    }
                    return NoContent();
                }
                return BadRequest("Invalid date format.");
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "RadiationMeasurementsController -> Year() -> Returned status code 500.");
                return StatusCode(500);
            }
        }
    }
}