const base = "http://localhost:8000/api/RadiationMeasurements"

export function getChartData(date, timespan) {
    const url = `${base}/${timespan}?date=${date}`;
    return fetch(url, {
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        }
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
        .catch(error => {
            throw error;
        });
}