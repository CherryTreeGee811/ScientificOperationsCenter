import { getChartDataForDay } from '../radiationMeasurementsDay.mjs';
import { getChartDataForMonth } from '../radiationMeasurementsMonth.mjs';
import { getChartDataForYear } from '../radiationMeasurementsYear.mjs';


export function handleRadiationMeasurementsRoutes(path, contentDiv) {
    const controller = "RadiationMeasurements";
    const measure = "Total Radiation";
    switch (path) {
        case '/radiation-measurements':
            loadTemplate("radiation-measurements/index.html", contentDiv);
            break;
        case '/radiation-measurements/day':
            loadTemplate("radiation-measurements/day.html", contentDiv);
            getChartDataForDay();
            break;
        case '/radiation-measurements/month':
            loadTemplate("radiation-measurements/month.html", contentDiv);
            getChartDataForMonth();
            break;
        case '/radiation-measurements/year':
            loadTemplate("radiation-measurements/year.html", contentDiv);
            getChartDataForYear();
            break;
        default:
            contentDiv.innerHTML = `<h1>404 Not Found</h1>`;
    }
}


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