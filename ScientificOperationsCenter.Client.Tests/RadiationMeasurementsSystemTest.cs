using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScientificOperationsCenter.Client.Tests.Shared;
using System.Drawing;


namespace ScientificOperationsCenter.System.Tests
{
    public class RadiationMeasurementsSystemTest
    {
        private ChromeDriver _driver { get; set; }
        private WebDriverWait _wait { get; set; }
        private MockScientificOperationsCenterAPI _mockAPI { get; set; }
        private HttpClient _httpClient { get; set; }


        [SetUp]
        public void SetUp()
        {
            _mockAPI = new MockScientificOperationsCenterAPI();
            _mockAPI.Start();
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8000") };

            ChromeOptions options = new ChromeOptions { AcceptInsecureCertificates = true };
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument($"--window-size={Display.DesktopWidth},{Display.DesktopHeight}");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            NavigateToBaseUrl();
        }


        private void NavigateToBaseUrl()
        {
            _driver.Navigate().GoToUrl(AppServer.ClientURL);
            _driver.Manage().Window.Size = new Size(Display.DesktopWidth, Display.DesktopHeight);
        }


        private IWebElement SafeFindElement(By by)
        {
            IWebElement element = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    element = _driver.FindElement(by);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(500);
                }
            }
            return element;
        }


        private long GetChartDataPointsCount()
        {
            try
            {
                return (long)((IJavaScriptExecutor)_driver).ExecuteScript(@"
                    const chart = Chart.instances[0]; // Assuming the first chart instance
                    return chart ? chart.data.datasets[0].data.length : 0;
                ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart data points: {ex.Message}");
                return 0;
            }
        }


        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
            _mockAPI.Stop();
            _httpClient.Dispose();
        }


        [Test]
        public void UserNavigatesToRadiationMeasurementsForDayPage()
        {
            var response = _httpClient.GetAsync("/api/RadiationMeasurements/day?date=2024-10-08").Result;
            response.EnsureSuccessStatusCode();
            NavigateToRadiationMeasurementsPage("radiation-measurements-day-link", "Radiation Measurements by Hour Of Day");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("12:00"), "The first label should be '12:00'");
            Assert.That(chartData[0], Is.EqualTo(410), "The first data point should be '410'");
            Assert.That(chartLabels[1], Is.EqualTo("13:00"), "The second label should be '13:00'");
            Assert.That(chartData[1], Is.EqualTo(390), "The second data point should be '390'");
            Assert.That(chartLabels[2], Is.EqualTo("14:00"), "The third label should be '14:00'");
            Assert.That(chartData[2], Is.EqualTo(212), "The third data point should be '212'");
        }


        [Test]
        public void UserNavigatesToRadiationMeasurementsForMonthPage()
        {
            var response = _httpClient.GetAsync("/api/RadiationMeasurements/month?date=2024-10-01").Result;
            response.EnsureSuccessStatusCode();
            NavigateToRadiationMeasurementsPage("radiation-measurements-month-link", "Radiation Measurements by Day Of Month");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("8"), "The first label should be '8'");
            Assert.That(chartData[0], Is.EqualTo(650), "The first data point should be '650'");
            Assert.That(chartLabels[1], Is.EqualTo("9"), "The second label should be '9'");
            Assert.That(chartData[1], Is.EqualTo(880), "The second data point should be '880'");
            Assert.That(chartLabels[2], Is.EqualTo("10"), "The third label should be '10'");
            Assert.That(chartData[2], Is.EqualTo(520), "The third data point should be '520'");
        }


        [Test]
        public void UserNavigatesToRadiationMeasurementsForYearPage()
        {
            var response = _httpClient.GetAsync("/api/RadiationMeasurements/year?date=2024-01-01").Result;
            response.EnsureSuccessStatusCode();
            NavigateToRadiationMeasurementsPage("radiation-measurements-year-link", "Radiation Measurements by Month Of Year");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("January"), "The first label should be 'January'");
            Assert.That(chartData[0], Is.EqualTo(1410), "The first data point should be '1410'");
            Assert.That(chartLabels[1], Is.EqualTo("February"), "The second label should be 'February'");
            Assert.That(chartData[1], Is.EqualTo(1390), "The second data point should be '1390'");
            Assert.That(chartLabels[2], Is.EqualTo("March"), "The third label should be 'March'");
            Assert.That(chartData[2], Is.EqualTo(1290), "The third data point should be '1290'");
        }


        private void NavigateToRadiationMeasurementsPage(string pageLinkID, string expectedPageTitle)
        {
            var radiationMeasurementsLink = SafeFindElement(By.Id("radiation-measurements-link"));
            new Actions(_driver).ScrollToElement(radiationMeasurementsLink).Perform();
            var radiationMeasurementsLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(radiationMeasurementsLink));
            radiationMeasurementsLinkElem.Click();

            var radiationMeasurementsPageLink = SafeFindElement(By.Id(pageLinkID));
            var radiationMeasurementsPageLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(radiationMeasurementsPageLink));
            radiationMeasurementsPageLinkElem.Click();

            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
            var pageTitle = Utilities.SafeFindElement(By.CssSelector("h1"), _driver).Text;
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("chart")));
            var chartCanvas = Utilities.SafeFindElement(By.Id("chart"), _driver);

            Assert.IsTrue(chartCanvas.Displayed, "The chart canvas should exist.");
            Assert.That(pageTitle, Is.EqualTo(expectedPageTitle));
            _wait.Until(driver => Utilities.GetDisplayedChartLabels(_driver).Count == 3);
            _wait.Until(driver => Utilities.GetDisplayedChartData(_driver).Count == 3);
            var chartDatasetLabel = Utilities.GetDisplayedChartDataSetLabel(_driver);
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels.Count, Is.GreaterThan(1), "The chart should have more than one label.");
            Assert.That(chartLabels.Count, Is.EqualTo(3), "The chart should have three labels exactly.");
            Assert.That(chartData.Count, Is.GreaterThan(1), "The chart should have more than one data point.");
            Assert.That(chartData.Count, Is.EqualTo(3), "The chart should have three data points exactly.");
            Assert.False(chartDatasetLabel.IsNullOrEmpty(), "The chart should have a dataset label");
            Assert.That(chartDatasetLabel, Is.EqualTo("Total Radiation"), "The dataset label should be 'Total Radiation'");
        }
    }
}