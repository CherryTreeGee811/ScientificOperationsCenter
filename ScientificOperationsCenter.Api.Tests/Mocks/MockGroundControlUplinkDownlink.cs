using ScientificOperationsCenter.Api.Models;


namespace ScientificOperationsCenter.Api.Tests.Mocks
{
    internal class MockGroundControlUplinkDownlink
    {
        private readonly SpacecraftPayload[] _temperaturesArray;
        private readonly SpacecraftPayload[] _radiationMeasurementsArray;


        public MockGroundControlUplinkDownlink()
        {
            _temperaturesArray =
            [
                new() { DateTime = "2020-09-05 04:00:03 AM", DataType = "TemperatureReading", Data = "20", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-09-05 03:15:00 PM", DataType = "TemperatureReading", Data = "15", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-09-07 12:00:02 PM", DataType = "TemperatureReading", Data = "-10", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-08 04:00:00 PM", DataType = "TemperatureReading", Data = "-4", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-08 07:00:09 PM", DataType = "TemperatureReading", Data = "12", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-08 12:00:40 PM", DataType = "TemperatureReading", Data = "11", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-09 09:30:30 PM", DataType = "TemperatureReading", Data = "12", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-09 09:00:20 PM", DataType = "TemperatureReading", Data = "10", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-09 06:00:10 AM", DataType = "TemperatureReading", Data = "9", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-11-02 09:10:11 AM", DataType = "TemperatureReading", Data = "-2", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-11-03 04:30:22 AM", DataType = "TemperatureReading", Data = "5", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-12-21 04:50:33 AM", DataType = "TemperatureReading", Data = "8", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-12-10 04:10:22 AM", DataType = "TemperatureReading", Data = "1", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-12-08 04:30:33 AM", DataType = "TemperatureReading", Data = "5", CRC = "A1B2C3D4" },
                new() { DateTime = "2021-01-03 05:30:33 AM", DataType = "TemperatureReading", Data = "15", CRC = "A1B2C3D4" },
                new() { DateTime = "2021-01-05 03:30:34 AM", DataType = "TemperatureReading", Data = "5", CRC = "A1B2C3D4" }
            ];


            _radiationMeasurementsArray =
            [
                new() { DateTime = "2020-09-08 04:00:40 PM", DataType = "RadiationReading", Data = "100", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-09-08 03:00:23 PM", DataType = "RadiationReading", Data = "140", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-09-08 12:00:08 PM", DataType = "RadiationReading", Data = "162", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-08 04:00:12 PM", DataType = "RadiationReading", Data = "100", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-08 07:00:34 PM", DataType = "RadiationReading", Data = "120", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-08 12:00:42 PM", DataType = "RadiationReading", Data = "190", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-09 09:30:21 PM", DataType = "RadiationReading", Data = "120", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-09 09:00:21 PM", DataType = "RadiationReading", Data = "110", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-10-09 06:00:00 AM", DataType = "RadiationReading", Data = "160", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-11-02 09:10:52 AM", DataType = "RadiationReading", Data = "100", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-11-03 02:30:01 AM", DataType = "RadiationReading", Data = "200", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-12-01 04:20:32 AM", DataType = "RadiationReading", Data = "120", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-12-02 04:10:01 AM", DataType = "RadiationReading", Data = "132", CRC = "A1B2C3D4" },
                new() { DateTime = "2020-12-05 07:30:02 AM", DataType = "RadiationReading", Data = "126", CRC = "A1B2C3D4" },
                new() { DateTime = "2021-01-03 04:30:08 AM", DataType = "RadiationReading", Data = "200", CRC = "A1B2C3D4" },
                new() { DateTime = "2021-01-05 04:30:02 AM", DataType = "RadiationReading", Data = "200", CRC = "A1B2C3D4" }
            ];
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
