import { getChartData } from './api.mjs';
import { generateChart } from './chart.mjs';
import { validateDate } from '../input-validator.mjs';


/**
 * @module year
 * @description This module handles the loading and visualization of temperature data
 * for a specific year. It fetches data from the API and generates a chart based on the retrieved data.
 *
 * Dependencies:
 * - `getChartData` from the `api.mjs` module for fetching data.
 * - `generateChart` from the `chart.mjs` module for rendering the chart.
 */


/**
 * Loads temperature data for a specific year and generates a chart.
 * 
 * This function retrieves temperature data for the specified date and 
 * timespan, then generates a chart using the retrieved data. It also 
 * handles loading and error messages in the UI.
 * 
 * @function loadTemperaturesForMonth
 * @param {string} date - The date for which to load temperature data. 
 *                        The expected format is 'YYYY-MM-DD'.
 * @returns {void} This function does not return a value. It updates the UI 
 * with loading and error messages as needed.
 * 
 * @example
 * // Load temperatures for the year provided in date param
 * loadTemperaturesForYear();
 */
export function loadTemperaturesForYear(date) {

    if (!validateDate(date)) {
        console.error("Invalid date provided");
        return
    }

    const timespan = 'year';
    const errorTextElement = document.getElementById("ErrorText");
    const loadingTextElement = document.getElementById("LoadingText");
    loadingTextElement.textContent = "Loading Chart ...";

    getChartData(date, timespan)
        .then(data => {
            if (data) {
                generateChart(data, "Time Frame (Month)");
                loadingTextElement.textContent = "";
            } else {
                errorTextElement.textContent = "No temperature records found for the selected date.";
            }
        })
        .catch(error => {
            const chart = document.getElementById('chart');
            chart.styles.display = "none";
            chart.ariaHidden = true;

            if (error.message.includes("404")) {
                errorTextElement.textContent = "Endpoint not found";
            } else if (error.message.includes("400")) {
                errorTextElement.textContent = "Invalid date was passed";
            } else if (error.message.includes("401")) {
                errorTextElement.textContent = "Unauthorized"; 
            } else if (error.message.includes("500")) {
                errorTextElement.textContent = "Internal server error";
            } else {
                errorTextElement.textContent = "Unknown error";
            }
            loadingTextElement.textContent = "";
        });
}