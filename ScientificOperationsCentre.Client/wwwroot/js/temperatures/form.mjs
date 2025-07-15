import { handleTemperaturesRoutes } from './router.mjs';
import { validateDate, validateTimeFrame } from '../input-validator.mjs';


export function loadFormJS() {
    const generateBtn = document.getElementById("generate-btn");

    generateBtn.addEventListener("click", function () {
        manageSubmission();
    });
}


function manageSubmission() {
    const dateElement = document.getElementById("date-input");
    const timeFrameElement = document.getElementById("time-frame-input");
    const url = new URL(window.location);

    if (validateDate(dateElement.value) && validateTimeFrame(timeFrameElement.value)) {
        switch (timeFrameElement.value) {
            case "day":
                url.pathname = '/temperatures/day';
                break;
            case "month":
                url.pathname = '/temperatures/month';
                break;
            case "year":
                url.pathname = '/temperatures/year';
                break;
        }

        url.searchParams.set('date', dateElement.value);
        window.history.pushState({}, '', url);
        const contentDiv = document.getElementById("content");
        handleTemperaturesRoutes(url.pathname, contentDiv);
    }
    else {
        console.error("You have provided invalid form inputs, please try again.");
    }
}