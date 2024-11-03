using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Drawing;


namespace ScientificOperationsCenter.System.Tests
{
    public class RadiationMeasurementsSystemTest
    {
        private ChromeDriver _driver { get; set; }
        private WebDriverWait _wait { get; set; }


        [SetUp]
        public void SetUp()
        {
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


        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }


        [Test]
        public void UserNavigatesToRadiationMeasurementsForDayPage()
        {
            NavigateToRadiationMeasurementsPage("radiation-measurements-day-link", "Radiation Measurements by Hour Of Day");
        }


        [Test]
        public void UserNavigatesToRadiationMeasurementsForMonthPage()
        {
            NavigateToRadiationMeasurementsPage("radiation-measurements-month-link", "Radiation Measurements by Day Of Month");
        }


        [Test]
        public void UserNavigatesToRadiationMeasurementsForYearPage()
        {
            NavigateToRadiationMeasurementsPage("radiation-measurements-year-link", "Radiation Measurements by Month Of Year");
        }


        private void NavigateToRadiationMeasurementsPage(string linkId, string expectedTitle)
        {
            var radiationMeasurementsLink = SafeFindElement(By.Id("radiation-measurements-link"));
            new Actions(_driver).ScrollToElement(radiationMeasurementsLink).Perform();
            var radiationMeasurementsLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(radiationMeasurementsLink));
            radiationMeasurementsLinkElem.Click();

            var radiationMeasurementsPageLink = SafeFindElement(By.Id(linkId));
            var radiationMeasurementsPageLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(radiationMeasurementsPageLink));
            radiationMeasurementsPageLinkElem.Click();

            var pageTitleElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
            var pageTitle = pageTitleElement.Text;
            var chartCanvas = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("chart")));
            bool chartInitialized = _wait.Until(driver =>
                (bool)_driver.ExecuteScript("return Chart.instances[0].data.datasets.length > 0;"));

            Assert.IsTrue(chartCanvas.Displayed, "The chart canvas exist.");
            Assert.That(pageTitle, Is.EqualTo(expectedTitle));
            var chartData = _driver.ExecuteScript("return Chart.instances[0].data.datasets[0].data.length;");
            Assert.IsTrue((long)chartData > 0, "The chart should have data points.");
        }
    }
}