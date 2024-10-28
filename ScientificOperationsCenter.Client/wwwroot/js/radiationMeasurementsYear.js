document.addEventListener("load", getChartData());


function getChartData() {
    const date = '2024-10-08';
    const url = `http://localhost:9000/api/RadiationMeasurements/year?date=${date}`;
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
            errorTextElement.textContent = "No radiation measurements found for select year";
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
    const context = document.getElementById('RadiationMeasurementsSumPerMonthOfTheYearLineChart').getContext('2d');
    const months = list.map(entry => entry.date);
    const totalRadiation = list.map(entry => entry.totalRadiation);
    new Chart(context, {
        type: "line",
        data: {
            labels: months,
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