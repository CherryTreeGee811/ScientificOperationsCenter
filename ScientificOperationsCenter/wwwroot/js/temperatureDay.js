document.addEventListener("load", getChartData());


function getChartData() {
    const date = '2024-10-08';
    const xhr = new XMLHttpRequest();
    xhr.open('GET', `/Temperatures/Day?date=${date}`, true);
    xhr.onload = function () {
        if (xhr.status === 200) {;
            const response = JSON.parse(xhr.responseText);
            generateChart(response);
        } else {
            console.error('Network response was not ok');
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