using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScientificOperationsCenter.Client.Tests.Shared;
using System.Drawing;


namespace ScientificOperationsCenter.System.Tests
{
    [TestFixture]
    public class TemperaturesSystemTest
    {
        private ChromeDriver _driver { get; set; }
        private WebDriverWait _wait { get; set; }
        private MockScientificOperationsCenterAPI _mockAPI { get; set; }


        [SetUp]
        public void SetUp()
        {
            _mockAPI = new MockScientificOperationsCenterAPI();
            _mockAPI.Start();

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
        }


        [Test]
        public void UserNavigatesToTemperaturesForDayPageNormally()
        {
            NavigateToTemperaturesPage("temperatures-day-link", "Temperatures throughout Day");
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
            NavigateToTemperaturesPage("temperatures-month-link", "Temperatures by Day of Month");
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
            NavigateToTemperaturesPage("temperatures-year-link", "Temperatures by Month of Year");
            var chartLabels = Utilities.GetDisplayedChartLabels(_driver);
            var chartData = Utilities.GetDisplayedChartData(_driver);
            Assert.That(chartLabels[0], Is.EqualTo("January"), "The first label should be 'January'");
            Assert.That(chartData[0], Is.EqualTo(130), "The first data point should be '130'");
            Assert.That(chartLabels[1], Is.EqualTo("February"), "The second label should be 'February'");
            Assert.That(chartData[1], Is.EqualTo(150), "The second data point should be '150'");
            Assert.That(chartLabels[2], Is.EqualTo("March"), "The third label should be 'March'");
            Assert.That(chartData[2], Is.EqualTo(170), "The third data point should be '170'");
        }


        private void NavigateToTemperaturesPage(string linkId, string expectedTitle)
        {
            var temperaturesLink = Utilities.SafeFindElement(By.Id("temperatures-link"), _driver);
            new Actions(_driver).ScrollToElement(temperaturesLink).Perform();
            var temperaturesLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(temperaturesLink));
            temperaturesLinkElem.Click();

            var temperaturesPageLink = Utilities.SafeFindElement(By.Id(linkId), _driver);
            var temperaturesPageLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(temperaturesPageLink));
            temperaturesPageLinkElem.Click();

            var pageTitleElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
            var pageTitle = pageTitleElement.Text;
            Assert.That(pageTitle, Is.EqualTo(expectedTitle));
            var chartCanvas = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("chart")));

            Assert.IsTrue(chartCanvas.Displayed, "The chart canvas should exist.");
            Assert.That(pageTitle, Is.EqualTo(expectedTitle));
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
            Assert.That(chartDatasetLabel, Is.EqualTo("Average Temperature"), "The dataset label should be 'Average Temperature'");
        }
    }
}
