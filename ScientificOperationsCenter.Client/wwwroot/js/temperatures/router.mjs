import { getChartDataForDay } from '../temperatureDay.mjs';
import { getChartDataForMonth } from '../temperatureMonth.mjs';
import { getChartDataForYear } from '../temperatureYear.mjs';


export function handleTemperaturesRoutes(path, contentDiv) {
    switch (path) {
        case '/temperatures':
            loadTemplate("temperatures/index.html", contentDiv);
            break;
        case '/temperatures/day':
            loadTemplate("temperatures/day.html", contentDiv);
            getChartDataForDay();
            break;
        case '/temperatures/month':
            loadTemplate("temperatures/month.html", contentDiv);
            getChartDataForMonth();
            break;
        case '/temperatures/year':
            loadTemplate("temperatures/year.html", contentDiv);
            getChartDataForYear();
            break;
        default:
            contentDiv.innerHTML = `<h1>404 Not Found</h1>`;
    }
}


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