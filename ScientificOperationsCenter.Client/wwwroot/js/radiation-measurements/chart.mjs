/**
 * @module chart
 * @description This module provides functions for generating charts using Chart.js.
 * It includes functionality to create line charts based on provided radiation data.
 */


/**
 * Generates a line chart displaying total radiation over time.
 * 
 * @function generateChart
 * @param {Array<Object>} list - An array of data objects, where each object contains:
 *   - {string} timeframe - The time period for the data point (e.g., '2024-10-08').
 *   - {number} totalRadiation - The total radiation measurement for the corresponding timeframe.
 * @returns {void} This function does not return a value; it directly renders the chart on the canvas.
 * 
 * @example
 * const data = [
 *   { Timeframe: '1', TotalRadiation: 100 },
 *   { Timeframe: '2', TotalRadiation: 120 },
 *   { Timeframe: '3', TotalRadiation: 160 },
 * ];
 * generateChart(data);
 */
export function generateChart(list) {
    const context = document.getElementById('chart').getContext('2d');
    const datetimes = list.map(entry => entry.timeFrame);
    const totalRadiation = list.map(entry => entry.totalRadiation);
    new Chart(context, {
        type: "line",
        data: {
            labels: datetimes,
            datasets: [{
                fill: false,
                label: 'Total Radiation',
                tension: 0,
                data: totalRadiation,
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