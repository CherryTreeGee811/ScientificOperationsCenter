document.addEventListener("load", getChartData());


function getChartData() {
    const date = '2024-10-08';
    const xhr = new XMLHttpRequest();
    xhr.open('GET', `/RadiationMeasurements/Day?date=${date}`, true);
    const errorTextElement = document.getElementById("ErrorText");
    const loadingTextElement = document.getElementById("LoadingText");
    loadingTextElement.textContent = "Loading Chart ...";
    xhr.onload = function () {
        if (xhr.status === 200) {
            loadingTextElement.textContent = "";
            const response = JSON.parse(xhr.responseText);
            generateChart(response);
        } else if (xhr.status == 404) {
            errorTextElement.textContent = "No radiation measurements found for select day";
            loadingTextElement.textContent = "";
        } else if (xhr.status == 400) {
            errorTextElement.textContent = "Invalid date was passed";
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
    const context = document.getElementById('RadiationMeasurementsSumPerHourOfTheDayLineChart').getContext('2d');
    const hours = list.map(entry => entry.hour);
    const totalRadiation = list.map(entry => entry.totalRadiation);
    new Chart(context, {
        type: "line",
        data: {
            labels: hours,
            datasets: [{
                fill: false,
                lineTension: 0,
                label: 'Total Radiation',
                data: totalRadiation,
                borderColor: "rgba(100,0,255,1.0)"
            }]
        },
        options: {
            scales: {
                responsive: true,
                y: {
                    ticks: {
                        min: 0,
                        max: 20,
                    }
                }
            }
        }
    });
}