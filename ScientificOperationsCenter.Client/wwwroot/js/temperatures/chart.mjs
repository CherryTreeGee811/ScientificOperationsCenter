/**
 * @module chart
 * @description This module provides functions for generating charts using Chart.js.
 * It includes functionality to create line charts based on provided data.
 */


/**
 * Generates a line chart displaying average temperatures over time.
 * 
 * @function generateChart
 * @param {Array<Object>} list - An array of data objects, where each object contains:
 *   - {string} timeframe - The time period for the data point (e.g., '2024-10-08').
 *   - {number} averageTemperature - The average temperature for the corresponding timeframe.
 * @returns {void} This function does not return a value; it directly renders the chart on the canvas.
 *
 * @example
 * const data = [
 *   { Timeframe: '2024-10-01', AverageTemperature: 15 },
 *   { Timeframe: '2024-10-02', AverageTemperature: 17 },
 *   { Timeframe: '2024-10-03', AverageTemperature: 16 },
 * ];
 * generateChart(data);
 */
export function generateChart(list) {
    const context = document.getElementById('chart').getContext('2d');
    const datetimes = list.map(entry => entry.Timeframe);
    const averageTemperatures = list.map(entry => entry.AverageTemperature);
    new Chart(context, {
        type: "line",
        data: {
            labels: datetimes,
            datasets: [{
                fill: false,
                tension: 0,
                label: 'Average Temperature',
                data: averageTemperatures,
                borderColor: "rgba(100,0,255,1.0)"
            }]
        },
        options: {
            scales: {
                responsive: true,
                y: {
                    ticks: {
                        min: 0,
                        max: 20,
                    }
                }
            }
        }
    });
}