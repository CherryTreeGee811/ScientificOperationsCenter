import { loadTemplate } from '../router.mjs';
import { loadFormJS } from './form.mjs';
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
    const urlParams = new URLSearchParams(window.location.search);
    const dateParam = urlParams.get('date');
    switch (path) {
        case '/radiation-measurements':
            loadTemplate("radiation-measurements/form.html", contentDiv).then(() => {
                return loadFormJS()
            }).catch((error) => {
                console.error('Error loading form js:', error);
            });
            break;
        case '/radiation-measurements/day':
            loadTemplate("radiation-measurements/day.html", contentDiv).then(() => {
                return loadRadiationMeasurementsForDay(dateParam)
            }).catch((error) => {
                console.error('Error loading radiation measurements for day:', error);
            });
            break;
        case '/radiation-measurements/month':
            loadTemplate("radiation-measurements/month.html", contentDiv).then(() => {
                return loadRadiationMeasurementsForMonth(dateParam)
            }).catch((error) => {
                console.error('Error loading radiation measurements for month:', error);;
            });
            break;
        case '/radiation-measurements/year':
            loadTemplate("radiation-measurements/year.html", contentDiv).then(() => {
                return loadRadiationMeasurementsForYear(dateParam)
            }).catch((error) => {
                console.error('Error loading radiation measurements for year:', error);
            });
            break;
        default:
            contentDiv.innerHTML = `<h1>404 Not Found</h1>`;
    }
}