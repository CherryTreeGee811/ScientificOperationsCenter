export function generateChart(list) {
    const context = document.getElementById('chart').getContext('2d');
    const datetimes = list.map(entry => entry.timeframe);
    const totalRadiation = list.map(entry => entry.totalRadiation);
    new Chart(context, {
        type: "line",
        data: {
            labels: datetimes,
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