using ScientificOperationsCenter.Api.ViewModels;
using System.Net;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;


namespace ScientificOperationsCenter.Client.Tests.Shared
{
    public class MockScientificOperationsCenterAPI
    {
        private WireMockServer _server;


        public WireMockServer Start()
        {
            _server = WireMockServer.Start(new WireMock.Settings.WireMockServerSettings
            {
                Urls = new[] { "http://localhost:8000" }
            });

            SetupMappingsForRadiationMeasurements();
            SetupMappingsForTemperatures();
            return _server;
        }


        #region SetupMappingsForRadiationMeasurements
        private void SetupMappingsForRadiationMeasurements()
        {
            #region MockData
            var mockRadiationMeasurementsDayResponseData = new List<RadiationMeasurementsViewModel>
            {
                new RadiationMeasurementsViewModel { TimeFrame = "12:00", TotalRadiation = 410 },
                new RadiationMeasurementsViewModel { TimeFrame = "13:00", TotalRadiation = 390 },
                new RadiationMeasurementsViewModel { TimeFrame = "14:00", TotalRadiation = 212 }
            };

            var mockRadiationMeasurementsMonthResponseData = new List<RadiationMeasurementsViewModel>
            {
                new RadiationMeasurementsViewModel { TimeFrame = "8", TotalRadiation = 650 },
                new RadiationMeasurementsViewModel { TimeFrame = "9", TotalRadiation = 880 },
                new RadiationMeasurementsViewModel { TimeFrame = "10", TotalRadiation = 520 }
            };

            var mockRadiationMeasurementsYearResponseData = new List<RadiationMeasurementsViewModel>
            {
                new RadiationMeasurementsViewModel { TimeFrame = "January", TotalRadiation = 1410 },
                new RadiationMeasurementsViewModel { TimeFrame = "February", TotalRadiation = 1390 },
                new RadiationMeasurementsViewModel { TimeFrame = "March", TotalRadiation = 1290 },
            };
            #endregion


            #region Endpoints
            _server
            .Given(Request.Create()
              .WithPath("/api/RadiationMeasurements/day")
              .UsingOptions())
            .RespondWith(Response.Create()
                 .WithStatusCode(HttpStatusCode.OK)
                 .WithHeader("Access-Control-Allow-Origin", "*")
                 .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
                 .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/RadiationMeasurements/day")
              .WithParam("date", "2024-10-08")
              .UsingGet())
              .RespondWith(Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
              .WithBodyAsJson(mockRadiationMeasurementsDayResponseData)
              .WithHeader("Access-Control-Allow-Origin", "*")
              .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
              .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/RadiationMeasurements/month")
              .UsingOptions())
            .RespondWith(Response.Create()
                 .WithStatusCode(HttpStatusCode.OK)
                 .WithHeader("Access-Control-Allow-Origin", "*")
                 .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
                 .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/RadiationMeasurements/month")
              .WithParam("date", "2024-10-08")
              .UsingGet())
              .RespondWith(Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
              .WithBodyAsJson(mockRadiationMeasurementsMonthResponseData)
              .WithHeader("Access-Control-Allow-Origin", "*")
              .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
              .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
           .Given(Request.Create()
               .WithPath("/api/RadiationMeasurements/year")
               .UsingOptions())
           .RespondWith(Response.Create()
               .WithStatusCode(HttpStatusCode.OK)
               .WithHeader("Access-Control-Allow-Origin", "*")
               .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
               .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/RadiationMeasurements/year")
              .WithParam("date", "2024-10-08")
              .UsingGet())
            .RespondWith(Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBodyAsJson(mockRadiationMeasurementsYearResponseData)
              .WithHeader("Access-Control-Allow-Origin", "*")
              .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
              .WithHeader("Access-Control-Allow-Headers", "Content-Type"));
            #endregion
        }
        #endregion


        #region SetupMappingsForTemperatures
        private void SetupMappingsForTemperatures()
        {
            #region MockData
            var mockTemperaturesDayResponseData = new List<TemperaturesViewModel>
            {
                new TemperaturesViewModel { TimeFrame = "12:00", AverageTemperature = 120 },
                new TemperaturesViewModel { TimeFrame = "13:00", AverageTemperature = 110 },
                new TemperaturesViewModel { TimeFrame = "14:00", AverageTemperature = 130 }
            };

            var mockTemperaturesMonthResponseData = new List<TemperaturesViewModel>
            {
                new TemperaturesViewModel { TimeFrame = "8", AverageTemperature = 190 },
                new TemperaturesViewModel { TimeFrame = "9", AverageTemperature = 185 },
                new TemperaturesViewModel { TimeFrame = "10", AverageTemperature = 160 }
            };

            var mockTemperaturesYearResponseData = new List<TemperaturesViewModel>
            {
                new TemperaturesViewModel { TimeFrame = "January", AverageTemperature = 130 },
                new TemperaturesViewModel { TimeFrame = "February", AverageTemperature = 150 },
                new TemperaturesViewModel { TimeFrame = "March", AverageTemperature = 170 },
            };
            #endregion


            #region Endpoints
            _server
           .Given(Request.Create()
             .WithPath("/api/Temperatures/day")
             .UsingOptions())
           .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
                .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/Temperatures/day")
              .WithParam("date", "2024-10-08")
              .UsingGet())
              .RespondWith(Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
              .WithBodyAsJson(mockTemperaturesDayResponseData)
              .WithHeader("Access-Control-Allow-Origin", "*")
              .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
              .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/Temperatures/month")
              .UsingOptions())
            .RespondWith(Response.Create()
                 .WithStatusCode(HttpStatusCode.OK)
                 .WithHeader("Access-Control-Allow-Origin", "*")
                 .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
                 .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/Temperatures/month")
              .WithParam("date", "2024-10-08")
              .UsingGet())
              .RespondWith(Response.Create()
            .WithStatusCode(HttpStatusCode.OK)
              .WithBodyAsJson(mockTemperaturesMonthResponseData)
              .WithHeader("Access-Control-Allow-Origin", "*")
              .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
              .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
           .Given(Request.Create()
               .WithPath("/api/Temperatures/year")
               .UsingOptions())
           .RespondWith(Response.Create()
               .WithStatusCode(HttpStatusCode.OK)
               .WithHeader("Access-Control-Allow-Origin", "*")
               .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
               .WithHeader("Access-Control-Allow-Headers", "Content-Type"));

            _server
            .Given(Request.Create()
              .WithPath("/api/Temperatures/year")
              .WithParam("date", "2024-10-08")
              .UsingGet())
            .RespondWith(Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBodyAsJson(mockTemperaturesYearResponseData)
              .WithHeader("Access-Control-Allow-Origin", "*")
              .WithHeader("Access-Control-Allow-Methods", "GET, OPTIONS")
              .WithHeader("Access-Control-Allow-Headers", "Content-Type"));
            #endregion

        }
        #endregion


        public void Stop()
        {
            _server.Stop();
            _server.Dispose();
        }
    }
}
