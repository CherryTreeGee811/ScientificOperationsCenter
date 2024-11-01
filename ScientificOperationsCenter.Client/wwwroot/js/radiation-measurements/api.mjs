/**
 * @module api
 * @description This module manages api requests for radiation measurements.
 * It provides functions to fetch radiation measurement data from the server.
 */


// API base URL for radiation measurements.
const base = "http://localhost:8000/api/RadiationMeasurements"


/**
 * Fetches chart data for a specific date and timespan.
 * 
 * @function getChartData
 * @param {string} date - The date for which to fetch the chart data in YYYY-MM-DD format.
 * @param {string} timespan - The timespan for the data (e.g., 'day', 'month', 'year').
 * @returns {Promise<Object|null>} A promise that resolves to the chart data object or null if no data is found.
 * @throws {Error} If the HTTP request fails or if the response status is not OK.
 * 
 * @example
 * getChartData('2024-10-08', 'day')
 *   .then(data => {
 *     console.log(data);
 *   })
 *   .catch(error => {
 *     console.error('Error fetching chart data:', error);
 *   });
 */
export function getChartData(date, timespan) {
    const url = `${base}/${timespan}?date=${date}`;
    return fetch(url, {
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (response.ok) {
                if (response.status === 204) {
                    return null;
                } else {
                    return response.json();
                }
            } else {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
        })
        .catch(error => {
            throw error;
        });
}