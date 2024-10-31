import { handleTemperaturesRoutes, initTemperaturesLinkListeners } from './temperatures/router.mjs';
import { handleRadiationMeasurementsRoutes, initRadiationMeasurementsLinkListeners } from './radiation-measurements/router.mjs';


document.addEventListener("DOMContentLoaded", () => {
    const contentDiv = document.getElementById("content");


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


    function routeHandler() {
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
            default:
                contentDiv.innerHTML = `<h1>404 Not Found</h1>`;
        }
    }


    document.getElementById("home-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/');
        routeHandler();
    });


    document.getElementById("temperatures-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/temperatures');
        routeHandler();
    });


    initTemperaturesLinkListeners(contentDiv, routeHandler);


    document.getElementById("radiation-measurements-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/radiation-measurements');
        routeHandler();
    });


    initRadiationMeasurementsLinkListeners(contentDiv, routeHandler);


    document.getElementById("radiation-measurements-link").addEventListener("click", (e) => {
        e.preventDefault();
        window.history.pushState({}, '', '/radiation-measurements');
        routeHandler();
    });


    window.addEventListener("popstate", routeHandler);


    routeHandler();
});