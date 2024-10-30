import { getChartData } from './api.mjs';
import { generateChart } from './chart.mjs';


export function loadTemperaturesForDay() {
    const date = '2024-10-08';
    const timespan = 'day';
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
