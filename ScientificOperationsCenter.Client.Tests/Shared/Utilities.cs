using OpenQA.Selenium;


namespace ScientificOperationsCenter.Client.Tests.Shared
{
    public static class Utilities
    {
        public static string? GetDisplayedChartDataSetLabel(IWebDriver Driver)
        {
            try
            {
                // Execute JavaScript to get the chart dataset label
                return (string?)((IJavaScriptExecutor)Driver).ExecuteScript(@"
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


        public static IList<string?> GetDisplayedChartLabels(IWebDriver Driver)
        {
            try
            {
                // Execute JavaScript to get the chart labels
                if (((IJavaScriptExecutor)Driver).ExecuteScript(@"
                    const chart = Chart.instances[0];
                    return chart ? chart.data.labels : [];
                    ") is IList<object> data)
                {
                    // Convert the IList<object> to a List<string?>
                    return [.. data.Select(l => Convert.ToString(l))];
                }
                else
                {
                    Console.WriteLine("Error: Chart labels data is null.");
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
                // Execute JavaScript to get the chart data
                if (((IJavaScriptExecutor)Driver).ExecuteScript(@"
                    const chart = Chart.instances[0];
                    return chart ? chart.data.datasets[0].data : [];
                    ") is IList<object> data)
                {
                    // Convert the IList<object> to a List<double>
                    return [.. data.Select(Convert.ToDouble)];
                }
                else
                {
                    Console.WriteLine("Error: Chart data is null.");
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
