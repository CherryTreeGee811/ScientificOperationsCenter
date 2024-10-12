document.addEventListener("load", getChartData());


function getChartData() {
    const date = '2024-10-08';
    const xhr = new XMLHttpRequest();
    xhr.open('GET', `/Temperatures/Day?date=${date}`, true);
    const errorTextElement = document.getElementById("ErrorText");
    const loadingTextElement = document.getElementById("LoadingText");
    loadingTextElement.textContent = "Loading Chart ...";
    xhr.onload = function () {
        if (xhr.status === 200) {;
            loadingTextElement.textContent = "";
            try {
                const response = JSON.parse(xhr.responseText);
                generateChart(response);
            } catch {
                errorTextElement.textContent = "Invalid date passed";
            }
        } else if (xhr.status == 404) {
            errorTextElement.textContent = "No temperatures found for select day";
            loadingTextElement.textContent = "";
        } else if (xhr.status == 500) {
            errorTextElement.textContent = "Internal server error";
            loadingTextElement.textContent = "";
        } else {
            errorTextElement.textContent = "Unknown error";
            loadingTextElement.textContent = "";
        }
    };

    xhr.onerror = function () {
        console.error('Network error');
    };

    xhr.send();
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