// API base URL for login.
const base = "http://localhost:8000/auth/login";


export function getToken() {
    const body = JSON.stringify({
        username: 'sciops_test',
        password: 'Hello123*'
    });

    return fetch(base, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Accept-Language': 'en-US',
        },
        body: body,
    })
        .then(response => {
            if (response.ok) {
                if (response.status === 204) {
                    return null;
                } else {
                    return response.json();
                }
            } else {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
        })
        .then(data => {
            if (data && data.token) {
                document.cookie = `token=${data.token}; path=/; SameSite=Strict;`;
            }
        })
        .catch(error => {
            throw error;
        });
}