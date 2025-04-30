using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;
using Serilog;


namespace ScientificOperationsCenter.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class ReceiveController(
            IRadiationMeasurementsRepository radiationMeasurementsRepository,
            ITemperaturesRepository temperaturesRepository
        ) : ControllerBase
    {
        private readonly IRadiationMeasurementsRepository _radiationMeasurementsRepository = radiationMeasurementsRepository
            ?? throw new ArgumentNullException(nameof(radiationMeasurementsRepository));


        private readonly ITemperaturesRepository _temperaturesRepository = temperaturesRepository
            ?? throw new ArgumentNullException(nameof(temperaturesRepository));


        [HttpPost("receive")]
        public async Task<IActionResult> Index([FromBody] SpacecraftPayload spacecraftPayload)
        {
            // ToDo: Protect endpoint with auth
            if (spacecraftPayload == null)
            {
                return BadRequest("Payload values were not provided");
            }

            try
            {
                var isDateTime = DateTime.TryParse(spacecraftPayload.DateTime, out DateTime dateTime);
                if (!isDateTime) return BadRequest("Invalid DateTime Provided");

                var date = DateOnly.FromDateTime(dateTime);
                if (date == default) return BadRequest("Invalid Date Provided");

                var time = TimeOnly.FromDateTime(dateTime);
                if (date == default) return BadRequest("Invalid Time Provided");

                var isInteger = int.TryParse(spacecraftPayload.Data, out int data);
                if (!isInteger) return BadRequest("Invalid Data Provided");
                if (!Equals(spacecraftPayload.DataType, null))
                {
                    if (spacecraftPayload.DataType.Trim().ToLower().Equals("temperaturereading"))
                    {
                        var temperature = new Temperatures()
                        {
                            Id = 0,
                            Date = date,
                            Time = time,
                            TemperatureCelcius = data,
                        };
                        await _temperaturesRepository.AddTemperature(temperature);
                        return Ok();
                    }
                    else if (spacecraftPayload.DataType.Trim().ToLower().Equals("radiationreading"))
                    {
                        var radiationMeasurement = new RadiationMeasurements()
                        {
                            Id = 0,
                            Date = date,
                            Time = time,
                            Milligrays = data,
                        };

                        await _radiationMeasurementsRepository.AddRadiationMeasurement(radiationMeasurement);
                        return Ok();
                    }
                }
               
                return BadRequest("We are only receiving RadiationReading or TemperatureReading as dataType");
            }
            catch (Exception gEx)
            {
                Log.Error(gEx, "RadiationMeasurementsController -> Receive() -> Returned status code 500.");
                return StatusCode(500);
            }
        }

    }
}
