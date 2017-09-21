var myMovingAverage = function(arr, win) {
    var filtered = medianFilter(arr, 5);
    var prefill = new Array(win);
    prefill.fill(0);
    filtered = prefill.concat(filtered);
    averaged = movingAverage(filtered, win);
    return averaged.slice(win);
}

var ctx_count_percent = document.getElementById('chart_prof_percent').getContext('2d');


// global settings:

Chart.defaults.global.animation = false;
Chart.defaults.global.elements.line.tension = 0;

var chart_prof_percent = new Chart(ctx_count_percent, {
    type: 'line',
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    callback: function(value, index, values) { return value.toPrecision(2) + '%'; }
                }
            }]
        },
        tooltips: {
            callbacks: {
                label: function(tooltipItem, data) { return tooltipItem.yLabel.toFixed(2) + '%'; }
            }
        }
    }
});

var drawCharts = function(periods) {

    //fetch('http://enyo.gk2.sk:8080/data.json').then(function(response) {
    fetch('/json.aspx?getjson=getprofits&periods=' + periods).then(function (response) {
        return response.json();
    }).then(function(blockData) {
        blockData.BitlishUSD = blockData.BitlishUSD.sort(function (a, b) { return a.id - b.id; });
        blockData.BitlishEUR = blockData.BitlishEUR.sort(function (a, b) { return a.id - b.id; });
        blockData.CEXUSD = blockData.CEXUSD.sort(function (a, b) { return a.id - b.id; });
        blockData.CEXEUR = blockData.CEXEUR.sort(function (a, b) { return a.id - b.id; });
        blockData.CEXGBP = blockData.CEXGBP.sort(function (a, b) { return a.id - b.id; });


        if (periods > blockData.BitlishUSD.length) {
            periods = 0;
        }



        var count_BitlishUSD = blockData.BitlishUSD.map(function (item) { return item.p; });
        var count_BitlishEUR = blockData.BitlishEUR.map(function (item) { return item.p; });
        var count_CEXUSD = blockData.CEXUSD.map(function (item) { return item.p; });
        var count_CEXEUR = blockData.CEXEUR.map(function (item) { return item.p; });
        var count_CEXGBP = blockData.CEXGBP.map(function (item) { return item.p; });

        var data_count_percent = {
            labels: blockData.BitlishUSD.map(function (item) { return item.ts; }).slice(-periods),
            datasets: [{
                label: 'Bitlish USD',
                //data: myMovingAverage(count_BitlishUSD, 4).slice(-periods),
                data: count_BitlishUSD,
                backgroundColor: 'rgba(71, 65, 244, 0.1)',
                borderColor: 'rgba(71, 65, 244, 1)',
                borderWidth: 1,
                pointRadius:0

            },
            {
                label: 'Bitlish EUR',
                //data: myMovingAverage(count_BitlishEUR, 4).slice(-periods),
                data: count_BitlishEUR,
                backgroundColor: 'rgba(32, 158, 28, 0.1)',
                borderColor: 'rgba(32, 158, 28, 1)',
                borderWidth: 1,
                pointRadius: 0
            },
            {
                label: 'Cex USD',
                //data: myMovingAverage(count_CEXUSD, 4).slice(-periods),
                data: count_CEXUSD,
                backgroundColor: 'rgba(244, 109, 65, 0.1)',
                borderColor: 'rgba(244, 109, 65, 1)',
                borderWidth: 1,
                pointRadius: 0
            },
            {
                label: 'Cex EUR',
                //data: myMovingAverage(count_CEXEUR, 4).slice(-periods),
                data: count_CEXEUR,
                backgroundColor: 'rgba(244, 169, 65, 0.1)',
                borderColor: 'rgba(244, 169, 65, 1)',
                borderWidth: 1,
                pointRadius: 0
            },
            {
                label: 'Cex GBP',
                //data: myMovingAverage(count_CEXGBP, 4).slice(-periods),
                data: count_CEXGBP,
                backgroundColor: 'rgba(241, 244, 65, 0.1)',
                borderColor: 'rgba(241, 244, 65, 1)',
                borderWidth: 1,
                pointRadius: 0
            }
            ]
        };

       
        chart_prof_percent.data = data_count_percent;
       

        chart_prof_percent.update();
       
    });
}
