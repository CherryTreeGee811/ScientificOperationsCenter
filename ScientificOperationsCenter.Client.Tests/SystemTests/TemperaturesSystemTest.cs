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
        private ChromeDriver Driver { get; set; }
        private WebDriverWait Wait { get; set; }
        private MockScientificOperationsCenterAPI MockAPI { get; set; }
        private HttpClient HttpClient { get; set; }


        [SetUp]
        public void SetUp()
        {
            MockAPI = new MockScientificOperationsCenterAPI();
            MockAPI.Start();
            HttpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8000") };

            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument($"--window-size={Display.DesktopWidth},{Display.DesktopHeight}");
            Driver = new ChromeDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            NavigateToBaseUrlAndLogin();
        }


        private void NavigateToBaseUrlAndLogin()
        {
            Driver.Navigate().GoToUrl(AppServer.ClientURL);
            Driver.Manage().Window.Size = new Size(Display.DesktopWidth, Display.DesktopHeight);
            var request = new HttpRequestMessage(HttpMethod.Options, "/auth/login");
            var response = HttpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            var loginLinkElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("login-link")));
            loginLinkElem.Click();
            var usernameInputElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("username-input")));
            var passwordInputElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("password-input")));
            var loginBtnElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login-btn")));

            usernameInputElem.SendKeys("sciops_test");
            passwordInputElem.SendKeys("Hello123*");
            loginBtnElem.Click();
        }


        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
            Driver.Dispose();
            MockAPI.Stop();
            HttpClient.Dispose();
        }


        [Test]
        public void UserNavigatesToTemperaturesForDayPageNormally()
        {
            var request = new HttpRequestMessage(HttpMethod.Options, "/api/Temperatures/day");
            var response = HttpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            NavigateToTemperaturesPage("Temperatures throughout Day", "day");
            var chartLabels = Utilities.GetDisplayedChartLabels(Driver);
            var chartData = Utilities.GetDisplayedChartData(Driver);
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
            var request = new HttpRequestMessage(HttpMethod.Options, "/api/Temperatures/month");
            var response = HttpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            NavigateToTemperaturesPage("Temperatures by Day of Month", "month");
            var chartLabels = Utilities.GetDisplayedChartLabels(Driver);
            var chartData = Utilities.GetDisplayedChartData(Driver);
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
            var request = new HttpRequestMessage(HttpMethod.Options, "/api/Temperatures/year");
            var response = HttpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            NavigateToTemperaturesPage("Temperatures by Month of Year", "year");
            var chartLabels = Utilities.GetDisplayedChartLabels(Driver);
            var chartData = Utilities.GetDisplayedChartData(Driver);
            Assert.That(chartLabels[0], Is.EqualTo("January"), "The first label should be 'January'");
            Assert.That(chartData[0], Is.EqualTo(130), "The first data point should be '130'");
            Assert.That(chartLabels[1], Is.EqualTo("February"), "The second label should be 'February'");
            Assert.That(chartData[1], Is.EqualTo(150), "The second data point should be '150'");
            Assert.That(chartLabels[2], Is.EqualTo("March"), "The third label should be 'March'");
            Assert.That(chartData[2], Is.EqualTo(170), "The third data point should be '170'");
        }


        private void NavigateToTemperaturesPage(string expectedPageTitle, string timeFrameValue)
        {
            var date = "2024-10-08";
            var temperaturesLinkElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("temperatures-link")));
            temperaturesLinkElem.Click();

            var formTitleElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("form-title")));
            var dateLabelElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("date-label")));
            var dateInputElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("date-input")));
            var timeFrameLabelElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("time-frame-label")));
            var timeFrameInputElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("time-frame-input")));
            var generateBtnElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("generate-btn")));
            Assert.That(formTitleElem.Displayed, Is.True, "The form-title should exist.");
            Assert.That(formTitleElem.Text, Is.EqualTo("Temperatures Form"), "Form page title should be 'Temperatures Form'");
            Assert.That(dateLabelElem.Displayed, Is.True, "The date-label should exist.");
            Assert.That(dateInputElem.Displayed, Is.True, "The date-input should exist.");
            Assert.That(timeFrameLabelElem.Displayed, Is.True   , "The time-frame-label should exist.");
            Assert.That(timeFrameInputElem.Displayed, Is.True, "The time-frame-input should exist.");
            Assert.That(generateBtnElem.Displayed, Is.True, "The generate-btn should exist.");
            Driver.ExecuteScript("arguments[0].value = '" + date + "';", dateInputElem);
            var timeFrameSelector = new SelectElement(timeFrameInputElem);
            timeFrameSelector.SelectByValue(timeFrameValue);
            generateBtnElem.Click();

            var pageTitleElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
            var chartCanvasElem = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("chart")));
            Wait.Until(Driver => Utilities.GetDisplayedChartLabels(Driver).Count == 3);
            Wait.Until(Driver => Utilities.GetDisplayedChartData(Driver).Count == 3);
            var chartDatasetLabel = Utilities.GetDisplayedChartDataSetLabel(Driver);
            var chartLabels = Utilities.GetDisplayedChartLabels(Driver);
            var chartData = Utilities.GetDisplayedChartData(Driver);

            Assert.That(chartCanvasElem.Displayed, Is.True, "The chart canvas should exist.");
            Assert.That(pageTitleElem.Text, Is.EqualTo(expectedPageTitle), $"h1 should display {expectedPageTitle}");
            Assert.That(chartLabels.Count, Is.GreaterThan(1), "The chart should have more than one label.");
            Assert.That(chartLabels.Count, Is.EqualTo(3), "The chart should have three labels exactly.");
            Assert.That(chartData.Count, Is.GreaterThan(1), "The chart should have more than one data point.");
            Assert.That(chartData.Count, Is.EqualTo(3), "The chart should have three data points exactly.");
            Assert.That(string.IsNullOrEmpty(chartDatasetLabel), Is.False, "The chart should have a dataset label");
            Assert.That(chartDatasetLabel, Is.EqualTo("Average Temperature"), "The dataset label should be 'Average Temperature'");
        }
    }
}
