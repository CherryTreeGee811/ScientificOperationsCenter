import { getChartData } from './api.mjs';
import { generateChart } from './chart.mjs';
import { validateDate } from '../input-validator.mjs';


/**
 * @module day
 * @description This module handles the loading and visualization of radiation measurement data
 * for a specific day. It fetches data from the API and generates a chart based on the retrieved data.
 * 
 * Dependencies:
 * - `getChartData` from the `api.mjs` module for fetching data.
 * - `generateChart` from the `chart.mjs` module for rendering the chart.
 */


/**
 * Loads radiation measurements for a specific day and generates a chart.
 * 
 * This function fetches radiation measurement data for a predefined date
 * and timespan, then generates a chart using the retrieved data. It also
 * handles loading states and displays error messages based on the outcome
 * of the data fetching process.
 * 
 * @function loadRadiationMeasurementsForDay
 * @param {string} date - The date for which to load radiation measurement data. 
 *                        The expected format is 'YYYY-MM-DD'.
 * @returns {void} This function does not return a value; it directly updates
 * the UI with loading messages and chart data.
 * 
 * @example
 * // Load radiation measurements for the day, month, and year specified in the date param
 * loadRadiationMeasurementsForDay();
 */
export function loadRadiationMeasurementsForDay(date) {

    if (!validateDate(date)) {
        console.error("Invalid date provided");
        return
    }

    const timespan = 'day';
    const errorTextElement = document.getElementById("ErrorText");
    const loadingTextElement = document.getElementById("LoadingText");
    loadingTextElement.textContent = "Loading Chart ...";

    getChartData(date, timespan)
        .then(data => {
            if (data) {
                generateChart(data, "Time Frame (Hour)");
                loadingTextElement.textContent = "";
            } else {
                errorTextElement.textContent = "No radiation measurement records found for the selected date.";
            }
        })
        .catch(error => {
            const chart = document.getElementById('chart');
            chart.style.display = "none";
            chart.ariaHidden = true;

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