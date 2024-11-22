import { handleTemperaturesRoutes, initTemperaturesLinkListeners } from './temperatures/router.mjs';
import { handleRadiationMeasurementsRoutes, initRadiationMeasurementsLinkListeners } from './radiation-measurements/router.mjs';
import { getAccessTokenFromCookie } from './parser.mjs';
import { loadFormJS } from './login.mjs';


/**
 * Initializes the application when the DOM is fully loaded.
 * 
 * This function sets up the main content area, initializes event listeners for 
 * navigation links, and handles routing based on the current URL path. It also 
 * loads the appropriate templates and data for temperature and radiation measurements.
 * 
 * @function
 * @returns {void} This function does not return a value.
 */
document.addEventListener("DOMContentLoaded", () => {
    const contentDiv = document.getElementById("content");

    // Event listener for the home link
    document.getElementById("home-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/');
        routeHandler(contentDiv);
    });


    // Event listener for the login link
    document.getElementById("login-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/login');
        routeHandler(contentDiv);
    });


    // Event listener for the temperatures link
    document.getElementById("temperatures-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/temperatures');
        routeHandler(contentDiv);
    });


    // Initialize link listeners for temperature measurements
    initTemperaturesLinkListeners(contentDiv, routeHandler);


    // Event listener for the radiation measurements link
    document.getElementById("radiation-measurements-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/radiation-measurements');
        routeHandler(contentDiv);
    });


    // Initialize link listeners for radiation measurements
    initRadiationMeasurementsLinkListeners(contentDiv, routeHandler);


    // Handle browser back/forward navigation
    window.addEventListener("popstate", routeHandler);


    // Initial route handling
    routeHandler(contentDiv);
});


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
* // Load the home template into the contentDiv
* loadTemplate("home.html", contentDiv);
*/
export function loadTemplate(templateName, contentDiv) {
    if (getAccessTokenFromCookie()) {
        const loginLinkElement = document.getElementById("login-link");
        loginLinkElement.style.display = 'none';
        loginLinkElement.ariaHidden = true;
    }

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


/**
* Handles routing based on the current URL path.
* 
* This function determines which template to load and which data to fetch based 
* on the current URL path. It updates the contentDiv with the appropriate template 
* and data for temperature and radiation measurements.
* 
* @function routeHandler
* @returns {void} This function does not return a value.
*/
export function routeHandler(contentDiv) {
    const path = window.location.pathname;
    switch (true) {
        case path == '/':
            loadTemplate("home.html", contentDiv);
            break;
        case path.startsWith('/radiation-measurements'):
            handleRadiationMeasurementsRoutes(path, contentDiv);
            break
        case path.startsWith('/temperatures'):
            handleTemperaturesRoutes(path, contentDiv);
            break;
        case path == '/login':
            loadTemplate("login.html", contentDiv).then(() => {
                return loadFormJS()
            }).catch((error) => {
                console.error('Error loading login form js:', error);
            });
            break;
        default:
            contentDiv.innerHTML = `<h1>404 Not Found</h1>`;
    }
}