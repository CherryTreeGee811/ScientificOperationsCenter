using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ScientificOperationsCenter.System.Tests;
using System.Drawing;


namespace ScientificOperationsCenter.Tests.SystemTests
{
    [TestFixture]
    public class TemperaturesSystemTest
    {
        private ChromeDriver _driver { get; set; }
        private WebDriverWait _wait { get; set; }


        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions { AcceptInsecureCertificates = true };
            options.AddArgument("--headless=new");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
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
                    Thread.Sleep(100);
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
        public void UserNavigatesToTemperaturesForDayPageNormally()
        {
            NavigateToTemperaturesPage("temperatures-day-link", "Temperatures throughout Day");
        }


        [Test]
        public void UserNavigatesToTemperaturesForMonthPageNormally()
        {
            NavigateToTemperaturesPage("temperatures-month-link", "Temperatures by Day of Month");
        }
        

        [Test]
        public void UserNavigatesToTemperaturesForYearPageNormally()
        {
            NavigateToTemperaturesPage("temperatures-year-link", "Temperatures by Month of Year");
        }


        private void NavigateToTemperaturesPage(string linkId, string expectedTitle)
        {
            var temperaturesLink = SafeFindElement(By.Id("temperatures-link"));
            new Actions(_driver).ScrollToElement(temperaturesLink).Perform();
            var temperaturesLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(temperaturesLink));
            temperaturesLinkElem.Click();

            var temperaturesPageLink = SafeFindElement(By.Id(linkId));
            var temperaturesPageLinkElem = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(temperaturesPageLink));
            temperaturesPageLinkElem.Click();

            var pageTitleElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
            var pageTitle = pageTitleElement.Text;
            Assert.That(pageTitle, Is.EqualTo(expectedTitle));
            var chartCanvas = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("chart")));

            Assert.IsTrue(chartCanvas.Displayed, "The chart canvas should exist.");
            Assert.That(pageTitle, Is.EqualTo(expectedTitle));
            var chartData = _driver.ExecuteScript("return Chart.instances[0].data.datasets[0].data.length;");
            Assert.IsTrue((long)chartData > 0, "The chart should have data points.");
        }
    }
}
