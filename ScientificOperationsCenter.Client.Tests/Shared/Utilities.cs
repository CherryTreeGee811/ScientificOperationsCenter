using OpenQA.Selenium;


namespace ScientificOperationsCenter.Client.Tests.Shared
{
    public static class Utilities
    {
        public static string GetDisplayedChartDataSetLabel(IWebDriver driver)
        {
            try
            {
                // Execute JavaScript to get the chart dataset label
                return (string)((IJavaScriptExecutor)driver).ExecuteScript(@"
                    const chart = Chart.instances[0];
                    return chart ? chart.data.datasets[0].label : '';
                ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart dataset label: {ex.Message}");
                return string.Empty;
            }
        }


        public static List<string?> GetDisplayedChartLabels(IWebDriver driver)
        {
            try
            {
                // Execute JavaScript to get the chart labels
                var data = (IList<object>)((IJavaScriptExecutor)driver).ExecuteScript(@"
                    const chart = Chart.instances[0];
                    return chart ? chart.data.labels : [];
                ");

                // Convert the IList<object> to a List<string?>
                return data.Select(l => Convert.ToString(l)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart label: {ex.Message}");
                return new List<string?>();
            }
        }


        public static List<double> GetDisplayedChartData(IWebDriver driver)
        {
            try
            {
                // Execute JavaScript to get the chart data
                var data = (IList<object>)((IJavaScriptExecutor)driver).ExecuteScript(@"
                    const chart = Chart.instances[0];
                    return chart ? chart.data.datasets[0].data : [];
                ");

                // Convert the IList<object> to a List<double>
                return data.Select(d => Convert.ToDouble(d)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chart data: {ex.Message}");
                return new List<double>();
            }
        }
    }
}
