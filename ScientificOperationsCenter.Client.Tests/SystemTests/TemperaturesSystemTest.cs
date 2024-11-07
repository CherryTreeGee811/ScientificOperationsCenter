using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ScientificOperationsCenter.Client.Tests.Shared;
using System.Drawing;


namespace ScientificOperationsCenter.Client.Tests.SystemTests
{
    [TestFixture]
    public class TemperaturesSystemTest
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
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            NavigateToBaseUrl();
        }


        private void NavigateToBaseUrl()
        {
            _driver.Navigate().GoToUrl(AppServer.ClientURL);
            _driver.Manage().Window.Size = new Size(Display.DesktopWidth, Display.DesktopHeight);
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
        public void UserNavigatesToTemperaturesForDayPageNormally()
        {
            var response = _httpClient.GetAsync("/api/Temperatures/day?date=2024-10-08").Result;
            response.EnsureSuccessStatusCode();
            NavigateToTemperaturesPage("Temperatures throughout Day", "day");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("12:00"), "The first label should be '12:00'");
            Assert.That(chartData[0], Is.EqualTo(120), "The first data point should be '120'");
            Assert.That(chartLabels[1], Is.EqualTo("13:00"), "The second label should be '13:00'");
            Assert.That(chartData[1], Is.EqualTo(110), "The second data point should be '110'");
            Assert.That(chartLabels[2], Is.EqualTo("14:00"), "The third label should be '14:00'");
            Assert.That(chartData[2], Is.EqualTo(130), "The third data point should be '130'");
        }


        [Test]
        public void UserNavigatesToTemperaturesForMonthPageNormally()
        {
            var response = _httpClient.GetAsync("/api/Temperatures/month?date=2024-10-08").Result;
            response.EnsureSuccessStatusCode();
            NavigateToTemperaturesPage("Temperatures by Day of Month", "month");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("8"), "The first label should be '8'");
            Assert.That(chartData[0], Is.EqualTo(190), "The first data point should be '190'");
            Assert.That(chartLabels[1], Is.EqualTo("9"), "The second label should be '9'");
            Assert.That(chartData[1], Is.EqualTo(185), "The second data point should be '185'");
            Assert.That(chartLabels[2], Is.EqualTo("10"), "The third label should be '10'");
            Assert.That(chartData[2], Is.EqualTo(160), "The third data point should be '160'");
        }


        [Test]
        public void UserNavigatesToTemperaturesForYearPageNormally()
        {
            var response = _httpClient.GetAsync("/api/Temperatures/year?date=2024-10-08").Result;
            response.EnsureSuccessStatusCode();
            NavigateToTemperaturesPage("Temperatures by Month of Year", "year");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("January"), "The first label should be 'January'");
            Assert.That(chartData[0], Is.EqualTo(130), "The first data point should be '130'");
            Assert.That(chartLabels[1], Is.EqualTo("February"), "The second label should be 'February'");
            Assert.That(chartData[1], Is.EqualTo(150), "The second data point should be '150'");
            Assert.That(chartLabels[2], Is.EqualTo("March"), "The third label should be 'March'");
            Assert.That(chartData[2], Is.EqualTo(170), "The third data point should be '170'");
        }


        private void NavigateToTemperaturesPage(string expectedPageTitle, string timeFrameValue)
        {
            var date = "08102024";
            var temperaturesLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("temperatures-link")));
            temperaturesLinkElem.Click();

            var formTitleElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("form-title")));
            var dateLabelElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("date-label")));
            var dateInputElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("date-input")));
            var timeFrameLabelElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("time-frame-label")));
            var timeFrameInputElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("time-frame-input")));
            var generateBtnElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("generate-btn")));
            Assert.IsTrue(formTitleElem.Displayed, "The form-title should exist.");
            Assert.That(formTitleElem.Text, Is.EqualTo("Temperatures Form"), "Form page title should be 'Temperatures Form'");
            Assert.IsTrue(dateLabelElem.Displayed, "The date-label should exist.");
            Assert.IsTrue(dateInputElem.Displayed, "The date-input should exist.");
            Assert.IsTrue(timeFrameLabelElem.Displayed, "The time-frame-label should exist.");
            Assert.IsTrue(timeFrameInputElem.Displayed, "The time-frame-input should exist.");
            Assert.IsTrue(generateBtnElem.Displayed, "The generate-btn should exist.");
            dateInputElem.SendKeys(date);
            var timeFrameSelector = new SelectElement(timeFrameInputElem);
            timeFrameSelector.SelectByValue(timeFrameValue);
            generateBtnElem.Click();

            var pageTitleElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
            var chartCanvasElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("chart")));
            _wait.Until(driver => Utilities.GetDisplayedChartLabels(_driver).Count == 3);
            _wait.Until(driver => Utilities.GetDisplayedChartData(_driver).Count == 3);
            var chartDatasetLabel = Utilities.GetDisplayedChartDataSetLabel(_driver);
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);

            Assert.IsTrue(chartCanvasElem.Displayed, "The chart canvas should exist.");
            Assert.That(pageTitleElem.Text, Is.EqualTo(expectedPageTitle), $"h1 should display {expectedPageTitle}");
            Assert.That(chartLabels.Count, Is.GreaterThan(1), "The chart should have more than one label.");
            Assert.That(chartLabels.Count, Is.EqualTo(3), "The chart should have three labels exactly.");
            Assert.That(chartData.Count, Is.GreaterThan(1), "The chart should have more than one data point.");
            Assert.That(chartData.Count, Is.EqualTo(3), "The chart should have three data points exactly.");
            Assert.False(chartDatasetLabel.IsNullOrEmpty(), "The chart should have a dataset label");
            Assert.That(chartDatasetLabel, Is.EqualTo("Average Temperature"), "The dataset label should be 'Average Temperature'");
        }
    }
}
