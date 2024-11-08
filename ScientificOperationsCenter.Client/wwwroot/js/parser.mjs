export function getAccessTokenFromCookie() {
    const name = 'token='; // Specify the cookie name with '='
    const decodedCookies = decodeURIComponent(document.cookie); // Decode any URL-encoded characters
    const cookiesArray = decodedCookies.split('; '); // Split cookies into an array

    // Iterate through the cookies to find the one with the key 'token'
    for (let i = 0; i < cookiesArray.length; i++) {
        if (cookiesArray[i].startsWith(name)) {
            return cookiesArray[i].substring(name.length); // Return the value of the 'token' cookie
        }
    }

    return null; // Return null if the cookie is not found
}