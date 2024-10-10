document.addEventListener("load", getChartData());


function getChartData() {
    const date = '2024-10-08';
    const xhr = new XMLHttpRequest();
    xhr.open('GET', `/RadiationMeasurements/Day?date=${date}`, true);
    xhr.onload = function () {
        if (xhr.status === 200) {
            ;
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