using OpenQA.Selenium;


namespace ScientificOperationsCentre.Client.Tests.Shared
{
    public static class Utilities
    {
        public static IWebElement FindElementWithRetry(By by, IWebDriver driver, int retries = 3)
        {
            for (int attempt = 0; attempt < retries; attempt++)
            {
                try
                {
                    var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    return wait.Until(OpenQA.Selenium.Support.UI.ExpectedConditions.ElementIsVisible(by));
                }
                catch (StaleElementReferenceException)
                {
                    if (attempt == retries - 1) throw;
                }
            }
            throw new NoSuchElementException($"Element not found after {retries} attempts: {by}");
        }
        public static string? GetDisplayedChartDataSetLabel(IWebDriver Driver)
        {
            try
            {
                // Use JS to check for chart element existence before extracting label
                var label = ((IJavaScriptExecutor)Driver).ExecuteScript(@"
                    const chartElem = document.getElementById('chart');
                    if (!chartElem) return null;
                    const chart = Chart.instances[0];
                    return chart ? chart.data.datasets[0].label : '';
                ");
                return label?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart dataset label: {ex.Message}");
                return string.Empty;
            }
        }


        public static IList<string?> GetDisplayedChartLabels(IWebDriver Driver)
        {
            try
            {
                // Use JS to check for chart element existence before extracting labels
                var result = ((IJavaScriptExecutor)Driver).ExecuteScript(@"
                    const chartElem = document.getElementById('chart');
                    if (!chartElem) return null;
                    const chart = Chart.instances[0];
                    return chart ? chart.data.labels : [];
                ");
                if (result is IList<object> data)
                {
                    return [.. data.Select(l => Convert.ToString(l))];
                }
                else if (result is IEnumerable<object> enumData)
                {
                    return enumData.Select(l => Convert.ToString(l)).ToList();
                }
                else
                {
                    Console.WriteLine("Error: Chart labels data is null or not an array.");
                    return [];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart label: {ex.Message}");
                return [];
            }
        }


        public static IList<double> GetDisplayedChartData(IWebDriver Driver)
        {
            try
            {
                // Use JS to check for chart element existence before extracting data
                var result = ((IJavaScriptExecutor)Driver).ExecuteScript(@"
                    const chartElem = document.getElementById('chart');
                    if (!chartElem) return null;
                    const chart = Chart.instances[0];
                    return chart ? chart.data.datasets[0].data : [];
                ");
                if (result is IList<object> data)
                {
                    return [.. data.Select(Convert.ToDouble)];
                }
                else if (result is IEnumerable<object> enumData)
                {
                    return enumData.Select(Convert.ToDouble).ToList();
                }
                else
                {
                    Console.WriteLine("Error: Chart data is null or not an array.");
                    return [];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart data: {ex.Message}");
                return [];
            }
        }
    }
}
