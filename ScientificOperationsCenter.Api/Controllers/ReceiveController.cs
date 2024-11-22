using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificOperationsCenter.Api.DAL.Interfaces;
using ScientificOperationsCenter.Api.Models;
using Serilog;


namespace ScientificOperationsCenter.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReceiveController : ControllerBase
    {
        // ToDo: Add Comments

        private readonly IRadiationMeasurementsRepository _radiationMeasurementsRepository;
        private readonly ITemperaturesRepository _temperaturesRepository;


        public ReceiveController(IRadiationMeasurementsRepository radiationMeasurementsRepository, ITemperaturesRepository temperaturesRepository)
        {
            _radiationMeasurementsRepository = radiationMeasurementsRepository ?? throw new ArgumentNullException(nameof(radiationMeasurementsRepository));
            _temperaturesRepository = temperaturesRepository ?? throw new ArgumentNullException(nameof(temperaturesRepository));
        }


        [AllowAnonymous]
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
                DateTime dateTime;
                int data;

                var isDateTime = DateTime.TryParse(spacecraftPayload.DateTime, out dateTime);
                if (!isDateTime) return BadRequest("Invalid DateTime Provided");

                var date = DateOnly.FromDateTime(dateTime);
                if (date == default) return BadRequest("Invalid Date Provided");

                var time = TimeOnly.FromDateTime(dateTime);
                if (date == default) return BadRequest("Invalid Time Provided");

                var isInteger = int.TryParse(spacecraftPayload.Data, out data);
                if (!isInteger) return BadRequest("Invalid Data Provided");
                
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
