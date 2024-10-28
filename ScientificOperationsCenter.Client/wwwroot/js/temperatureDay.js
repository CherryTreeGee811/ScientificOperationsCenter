document.addEventListener("load", getChartData());


function getChartData() {
    const date = '2024-10-08';
    const url = `https://localhost:5001/api/Temperatures/day?date=${date}`;
    const errorTextElement = document.getElementById("ErrorText");
    const loadingTextElement = document.getElementById("LoadingText");
    loadingTextElement.textContent = "Loading Chart ...";

    fetch(url, {
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
    })
    .then(data => {
        loadingTextElement.textContent = "";
        generateChart(data);
    })
    .catch(error => {
        if (error.message.includes("404")) {
            errorTextElement.textContent = "No temperatures found for select day";
        } else if (error.message.includes("400")) {
            errorTextElement.textContent = "Invalid date was passed";
        } else if (error.message.includes("500")) {
            errorTextElement.textContent = "Internal server error";
        } else {
            errorTextElement.textContent = "Unknown error";
        }
        loadingTextElement.textContent = "";
    });
}


function generateChart(list) {
    const context = document.getElementById('averageTemperaturesPerHourOfTheDayLineChart').getContext('2d');
    const hours = list.map(entry => entry.hour);
    const averageTemperatures = list.map(entry => entry.averageTemperature);
    new Chart(context, {
        type: "line",
        data: {
            labels: hours,
            datasets: [{
                fill: false,
                lineTension: 0,
                label: 'Average Temperature',
                data: averageTemperatures,
                borderColor: "rgba(100,0,255,1.0)"
            }]
        },
        options: {
            scales: {
                responsive: true,
                y: {
                    ticks: {
                        min: -20,
                        max: 20,
                    }
                }
            }
        }
    });
}