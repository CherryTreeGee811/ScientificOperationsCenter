import { getToken } from './api.mjs';


export function loadFormJS() {
    const loginBtn = document.getElementById("login-btn");

    loginBtn.addEventListener("click", function () {
        manageSubmission();
    });
}


function manageSubmission() {
    const usernameElement = document.getElementById("username-input");
    const passwordElement = document.getElementById("password-input");

    const result = getToken(usernameElement.value, passwordElement.value);

    if (result) {
        const loginLinkElement = document.getElementById("login-link");
        loginLinkElement.style.display = 'none';
        loginLinkElement.ariaHidden = true;
        const homeLinkElement = document.getElementById("home-link");
        loginLinkElement.addEventListener("load", homeLinkElement.click());
    } else {
        console.error("You have provided invalid credentials, please try again.");
    }
}