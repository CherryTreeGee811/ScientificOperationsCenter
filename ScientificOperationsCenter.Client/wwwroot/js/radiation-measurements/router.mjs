import { loadRadiationMeasurementsForDay } from './day.mjs';
import { loadRadiationMeasurementsForMonth } from './month.mjs';
import { loadRadiationMeasurementsForYear } from './year.mjs';


/**
 * @module router
 * 
 * This module handles routing for radiation measurement views based on the 
 * URL path. It loads the appropriate HTML templates and JavaScript modules for
 * radiation-meaurements' day, month, year, and index endpoints.
 * 
 * @dependencies
 * - `./day.mjs`: Function to load hourly radiation measurements for the day.
 * - `./month.mjs`: Function to load daily radiation measurements for the month.
 * - `./year.mjs`: Function to load monthly radiation measurements for the year.
 */


/**
 * Initializes click event listeners for radiation-measurements' links.
 * 
 * This function adds event listeners to the specified contentDiv to handle 
 * clicks on links for day, month, and year seen in radiation-measurements' index.html. When a 
 * link is clicked, it prevents the default action, updates the browser's 
 * history state, and calls the provided route handler to update the view.
 * 
 * @function initRadiationMeasurementsLinkListeners
 * @param {HTMLElement} contentDiv - The HTML element where the links are located.
 * @param {function} routeHandler - The function to call to handle routing.
 * @returns {void} This function does not return a value.
 * 
 * @example
 * // Initialize link listeners for radiation-measurements
 * initRadiationMeasurementsLinkListeners(contentDiv, () => handleRadiationMeasurementsRoutes(window.location.pathname, contentDiv));
 */
export function initRadiationMeasurementsLinkListeners(contentDiv, routeHandler) {
    contentDiv.addEventListener("click", (e) => {
        if (e.target.matches("#radiation-measurements-day-link")) {
            e.preventDefault();
            window.history.pushState({}, '', '/radiation-measurements/day');
            routeHandler();
        } else if (e.target.matches("#radiation-measurements-month-link")) {
            e.preventDefault();
            window.history.pushState({}, '', '/radiation-measurements/month');
            routeHandler();
        } else if (e.target.matches("#radiation-measurements-year-link")) {
            e.preventDefault();
            window.history.pushState({}, '', '/radiation-measurements/year');
            routeHandler();
        }
    });
}


/**
 * Handles routing for radiation-measurements based on its provided paths.
 * 
 * This function loads the appropriate HTML template and calls the corresponding 
 * function to load radiation measurement charts for a given time span (e.g. day, 
 * month, or year). It updates the content of the specified HTML element (`contentDiv`) 
 * with the loaded template and data.
 * 
 * @function handleRadiationMeasurementsRoutes
 * @param {string} path - The URL path to determine which template to load.
 * @param {HTMLElement} contentDiv - The HTML element where the template will be loaded.
 * @returns {void} This function does not return a value.
 * 
 * @example
 * // Handle routing for radiation-measurements
 * handleRadiationMeasurementsRoutes('/radiation-measurements/day', contentDiv);
 */
export function handleRadiationMeasurementsRoutes(path, contentDiv) {
    switch (path) {
        case '/radiation-measurements':
            loadTemplate("radiation-measurements/index.html", contentDiv);
            break;
        case '/radiation-measurements/day':
            loadTemplate("radiation-measurements/day.html", contentDiv);
            loadRadiationMeasurementsForDay();
            break;
        case '/radiation-measurements/month':
            loadTemplate("radiation-measurements/month.html", contentDiv);
            loadRadiationMeasurementsForMonth();
            break;
        case '/radiation-measurements/year':
            loadTemplate("radiation-measurements/year.html", contentDiv);
            loadRadiationMeasurementsForYear();
            break;
        default:
            contentDiv.innerHTML = `<h1>404 Not Found</h1>`;
    }
}


/**
 * Loads an HTML template and updates the specified contentDiv with the fetched content.
 * 
 * This function fetches the specified template from the server and updates the 
 * inner HTML of the provided contentDiv. If the fetch operation fails, it displays 
 * an error message in the contentDiv.
 * 
 * @function loadTemplate
 * @param {string} templateName - The name of the template file to load.
 * @param {HTMLElement} contentDiv - The HTML element where the template will be loaded.
 * @returns {void} This function does not return a value.
 * 
 * @example
 * // Load a specific template into the contentDiv
 * loadTemplate("radiation-measurements/day.html", contentDiv);
 */
function loadTemplate(templateName, contentDiv) {
    fetch(`/templates/${templateName}`)
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.text();
        })
        .then(html => {
            contentDiv.innerHTML = html;
        })
        .catch(error => {
            contentDiv.innerHTML = `<h1>Error loading template</h1><p>${error.message}</p>`;
        });
}