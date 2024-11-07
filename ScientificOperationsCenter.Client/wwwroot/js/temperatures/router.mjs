import { loadFormJS } from './form.mjs';
import { loadTemperaturesForDay } from './day.mjs';
import { loadTemperaturesForMonth } from './month.mjs';
import { loadTemperaturesForYear } from './year.mjs';


/**
 * @module router
 * 
 * This module handles routing for temperature views based on the 
 * URL path. It loads the appropriate HTML templates and JavaScript modules for
 * temperatures' day, month, year, and index endpoints.
 * 
 * @dependencies
 * - `./day.mjs`: Function to load average hourly temperatures for the day.
 * - `./month.mjs`: Function to load average daily temperatures for the month.
 * - `./year.mjs`: Function to load average monthly temperatures for the year.
 */



/**
 * Initializes click event listeners for temperatures' links.
 * 
 * This function adds event listeners to the specified contentDiv to handle 
 * clicks on links for day, month, and year seen in temperatures' index.html. When a 
 * link is clicked, it prevents the default action, updates the browser's 
 * history state, and calls the provided route handler to update the view.
 * 
 * @function initTemperaturesLinkListeners
 * @param {HTMLElement} contentDiv - The HTML element where the links are located.
 * @param {function} routeHandler - The function to call to handle routing.
 * @returns {void} This function does not return a value.
 * 
 * @example
 * // Initialize link listeners for temperatures
 * initTemperaturesLinkListeners(contentDiv, () => handleRadiationMeasurementsRoutes(window.location.pathname, contentDiv));
 */
export function initTemperaturesLinkListeners(contentDiv, routeHandler) {
    contentDiv.addEventListener("click", (e) => {
        if (e.target.matches("#temperatures-day-link")) {
            e.preventDefault();
            window.history.pushState({}, '', '/temperatures/day');
            routeHandler();
        } else if (e.target.matches("#temperatures-month-link")) {
            e.preventDefault();
            window.history.pushState({}, '', '/temperatures/month');
            routeHandler();
        } else if (e.target.matches("#temperatures-year-link")) {
            e.preventDefault();
            window.history.pushState({}, '', '/temperatures/year');
            routeHandler();
        }
    });
}


/**
 * Handles routing for temperatures based on its provided paths.
 * 
 * This function loads the appropriate HTML template and calls the corresponding 
 * function to load temperature charts for a given time span (e.g. day, 
 * month, or year). It updates the content of the specified HTML element (`contentDiv`) 
 * with the loaded template and data.
 * 
 * @function handleTemperaturesRoutes
 * @param {string} path - The URL path to determine which template to load.
 * @param {HTMLElement} contentDiv - The HTML element where the template will be loaded.
 * @returns {void} This function does not return a value.
 * 
 * @example
 * // Handle routing for temperatures
 * handleTemperaturesRoutes('/temperatures/day', contentDiv);
 */
export function handleTemperaturesRoutes(path, contentDiv) {
    const urlParams = new URLSearchParams(window.location.search);
    const dateParam = urlParams.get('date');
    switch (path) {
        case '/temperatures':
            loadTemplate("temperatures/form.html", contentDiv).then(() => {
                return loadFormJS()
            }).catch((error) => {
                console.error('Error loading form js:', error);
            });
            break;
        case '/temperatures/day':
            loadTemplate("temperatures/day.html", contentDiv).then(() => {
                return loadTemperaturesForDay(dateParam)
            }).catch((error) => {
                console.error('Error loading temperatures for day:', error);
            });
            break;
        case '/temperatures/month':
            loadTemplate("temperatures/month.html", contentDiv).then(() => {
                return loadTemperaturesForMonth(dateParam)
            }).catch((error) => {
                console.error('Error loading temperatures for month:', error);
            });
            break;
        case '/temperatures/year':
            loadTemplate("temperatures/year.html", contentDiv).then(() => {
                return loadTemperaturesForYear(dateParam)
            }).catch((error) => {
                console.error('Error loading temperatures for year:', error);
            });
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
 * loadTemplate("temperatures/day.html", contentDiv);
 */
function loadTemplate(templateName, contentDiv) {
    return fetch(`/templates/${templateName}`)
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.text();
        })
        .then(html => {
            contentDiv.innerHTML = html;
            return Promise.resolve();
        })
        .catch(error => {
            contentDiv.innerHTML = `<h1>Error loading template</h1><p>${error.message}</p>`;
            return Promise.reject(error);
        });
}