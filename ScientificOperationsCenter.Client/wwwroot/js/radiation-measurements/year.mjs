import { getChartData } from './api.mjs';
import { generateChart } from './chart.mjs';


/**
 * @module year
 * @description This module handles the loading and visualization of radiation measurement data
 * for a specific year. It fetches data from the API and generates a chart based on the retrieved data.
 * 
 * Dependencies:
 * - `getChartData` from the `api.mjs` module for fetching data.
 * - `generateChart` from the `chart.mjs` module for rendering the chart.
 */


/**
 * Loads radiation measurements for a specific year and generates a chart.
 * 
 * This function fetches radiation measurement data for a predefined date
 * and timespan, then generates a chart using the retrieved data. It also
 * handles loading states and displays error messages based on the outcome
 * of the data fetching process.
 * 
 * @function loadRadiationMeasurementsForYear
 * @returns {void} This function does not return a value; it directly updates
 * the UI with loading messages and chart data.
 * 
 * @example
 * // Load temperatures for the year 2024
 * loadRadiationMeasurementsForYear();
 */
export function loadRadiationMeasurementsForYear() {
    const date = '2024-01-01';
    const timespan = 'year';
    const errorTextElement = document.getElementById("ErrorText");
    const loadingTextElement = document.getElementById("LoadingText");
    loadingTextElement.textContent = "Loading Chart ...";

    getChartData(date, timespan)
        .then(data => {
            if (data) {
                generateChart(data);
                loadingTextElement.textContent = "";
            } else {
                errorTextElement.textContent = "No radiation measurement records found for the selected date.";
            }
        })
        .catch(error => {
            if (error.message.includes("404")) {
                errorTextElement.textContent = "Endpoint not found";
            } else if (error.message.includes("400")) {
                errorTextElement.textContent = "Invalid date was passed";
            } else if (error.message.includes("500")) {
                errorTextElement.textContent = "Internal server error";
            } else {
                errorTextElement.textContent = "Unknown error";
            }
            loadingTextElement.textContent = "";
        });
}