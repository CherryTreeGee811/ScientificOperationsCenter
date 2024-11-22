using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal class MockGroundControlUplinkDownlink
    {
        private readonly SpacecraftPayload[] _temperaturesArray;
        private readonly SpacecraftPayload[] _radiationMeasurementsArray;


        public MockGroundControlUplinkDownlink()
        {
            var temperatures = new List<SpacecraftPayload>()
            {
                new SpacecraftPayload { DateTime = "2020-09-05 04:00:03 AM", DataType="TemperatureReading", Data="20", CRC="A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-09-05 03:15 PM", DataType="TemperatureReading", Data="15", CRC="A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-09-07 12:00PM", DataType="TemperatureReading", Data="-10", CRC="A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-08 16:00", DataType = "TemperatureReading", Data = "-4", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-08 19:00", DataType = "TemperatureReading", Data = "12", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-08 12:00", DataType = "TemperatureReading", Data = "11", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-09 21:30", DataType = "TemperatureReading", Data = "12", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-09 21:00", DataType = "TemperatureReading", Data = "10", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-09 06:00", DataType = "TemperatureReading", Data = "9", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-11-02 09:10", DataType = "TemperatureReading", Data = "-2", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-11-03 04:30", DataType = "TemperatureReading", Data = "5", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-12-21 04:50", DataType = "TemperatureReading", Data = "8", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-12-10 04:10", DataType = "TemperatureReading", Data = "1", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-12-08 04:30", DataType = "TemperatureReading", Data = "5", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2021-01-03 05:30", DataType = "TemperatureReading", Data = "15", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2021-01-05 03:30", DataType = "TemperatureReading", Data = "5", CRC = "A1B2C3D4" }
            };


            var radiationMeasurements = new List<SpacecraftPayload>()
            {
                new SpacecraftPayload { DateTime = "2020-09-08 16:00", DataType = "RadiationReading", Data = "100", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-09-08 15:00", DataType = "RadiationReading", Data = "140", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-09-08 12:00", DataType = "RadiationReading", Data = "162", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-08 16:00", DataType = "RadiationReading", Data = "100", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-08 19:00", DataType = "RadiationReading", Data = "120", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-08 12:00", DataType = "RadiationReading", Data = "190", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-09 21:30", DataType = "RadiationReading", Data = "120", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-09 21:00", DataType = "RadiationReading", Data = "110", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-10-09 06:00", DataType = "RadiationReading", Data = "160", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-11-02 09:10", DataType = "RadiationReading", Data = "100", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-11-03 02:30", DataType = "RadiationReading", Data = "200", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-12-01 04:20", DataType = "RadiationReading", Data = "120", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-12-02 04:10", DataType = "RadiationReading", Data = "132", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2020-12-05 07:30", DataType = "RadiationReading", Data = "126", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2021-01-03 04:30", DataType = "RadiationReading", Data = "200", CRC = "A1B2C3D4" },
                new SpacecraftPayload { DateTime = "2021-01-05 04:30", DataType = "RadiationReading", Data = "200", CRC = "A1B2C3D4" }
            };

            _temperaturesArray = temperatures.ToArray();
            _radiationMeasurementsArray = radiationMeasurements.ToArray();
        }


        public SpacecraftPayload[] GetTemperatures()
        {
            return _temperaturesArray;
        }

        public SpacecraftPayload[] GetRadiationMeasurements()
        {
            return _radiationMeasurementsArray;
        }
    }
}
