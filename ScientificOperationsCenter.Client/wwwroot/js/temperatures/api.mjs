import { getAccessTokenFromCookie } from '../parser.mjs';


/**
 * @module api
 * @description This module manages api requests for temperatures.
 * It provides functions to fetch temperature data from the server.
 */


// API base URL for temperatures.
const base = "http://localhost:8000/api/Temperatures"


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
    const accessToken = getAccessTokenFromCookie();

    return fetch(url, {
        mode: 'cors',
        headers: {
            'Authorization': `Bearer ${accessToken}`,
            'Accept': 'application/json',
            'Accept-Language': 'en-US',
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