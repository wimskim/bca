var myMovingAverage = function(arr, win) {
    var filtered = medianFilter(arr, 5);
    var prefill = new Array(win);
    prefill.fill(0);
    filtered = prefill.concat(filtered);
    averaged = movingAverage(filtered, win);
    return averaged.slice(win);
}

var ctx_count_percent = document.getElementById('chart_prof_percent').getContext('2d');
var ctx_all_to_zar = document.getElementById('chart_all_to_zar').getContext('2d');


// global settings:

Chart.defaults.global.animation = false;
Chart.defaults.global.elements.line.tension = 0;

var chart_prof_percent = new Chart(ctx_count_percent, {
    type: 'line',
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    callback: function(value, index, values) { return value.toPrecision(2) + ' %'; }
                }
            }]
        },
        tooltips: {
            callbacks: {
                label: function(tooltipItem, data) { return tooltipItem.yLabel.toFixed(2) + ' %'; }
            }
        }
    }
});

var chart_all_to_zar = new Chart(ctx_all_to_zar, {
    type: 'line',
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    callback: function (value, index, values) { return value.toFixed(0) + ' ZAR'; }
                }
            }]
        },
        tooltips: {
            callbacks: {
                label: function (tooltipItem, data) { return tooltipItem.yLabel.toFixed(0) + ' ZAR'; }
            }
        }
    }
});

var drawCharts = function(days) {

    fetch('/json.aspx?getjson=getprofits&days=' + days).then(function (response) {
        return response.json();
    }).then(function(blockData) {
        blockData.BitlishUSD = blockData.BitlishUSD.sort(function (a, b) { return a.id - b.id; });
        blockData.BitlishEUR = blockData.BitlishEUR.sort(function (a, b) { return a.id - b.id; });
        blockData.CEXUSD = blockData.CEXUSD.sort(function (a, b) { return a.id - b.id; });
        blockData.CEXEUR = blockData.CEXEUR.sort(function (a, b) { return a.id - b.id; });
        blockData.CEXGBP = blockData.CEXGBP.sort(function (a, b) { return a.id - b.id; });

        var periods = blockData.BitlishUSD.length;

        //if (periods > blockData.BitlishUSD.length) {
        //    periods = 0;
        //}



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
       
        var count_luno_zar = blockData.BitlishUSD.map(function (item) { return item.bid; });
        var count_bitlish_usd_to_zar = blockData.BitlishUSD.map(function (item) { return (item.ask * item.rate); });
        var count_bitlish_eur_to_zar = blockData.BitlishEUR.map(function (item) { return (item.ask * item.rate); });
        var count_cex_usd_to_zar = blockData.CEXUSD.map(function (item) { return (item.ask * item.rate); });
        var count_cex_eur_to_zar = blockData.CEXEUR.map(function (item) { return (item.ask * item.rate); });
        var count_cex_gbp_to_zar = blockData.CEXGBP.map(function (item) { return (item.ask * item.rate); });

        var data_luno_zar = {
            labels: blockData.BitlishUSD.map(function (item) { return item.ts; }).slice(-periods),
            datasets: [{
                label: 'Luno ZAR',
                //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
                data: count_luno_zar,
                backgroundColor: 'rgba(244, 66, 232, 0.1)',
                borderColor: 'rgba(244, 66, 232, 1)',
                borderWidth: 1,
                pointRadius: 0

            },
            {
                label: 'Bitlish USD',
                //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
                data: count_bitlish_usd_to_zar,
                backgroundColor: 'rgba(71, 65, 244, 0.1)',
                borderColor: 'rgba(71, 65, 244, 1)',
                borderWidth: 1,
                pointRadius: 0

            },
            {
                label: 'Bitlish EUR',
                //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
                data: count_bitlish_eur_to_zar,
                backgroundColor: 'rgba(32, 158, 28, 0.1)',
                borderColor: 'rgba(32, 158, 28, 1)',
                borderWidth: 1,
                pointRadius: 0

            },
            {
                label: 'CEX USD',
                //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
                data: count_cex_usd_to_zar,
                backgroundColor: 'rgba(244, 109, 65, 0.1)',
                borderColor: 'rgba(244, 109, 65, 1)',
                borderWidth: 1,
                pointRadius: 0

            },
            {
                label: 'CEX EUR',
                //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
                data: count_cex_eur_to_zar,
                backgroundColor: 'rgba(244, 169, 65, 0.1)',
                borderColor: 'rgba(244, 169, 65, 1)',
                borderWidth: 1,
                pointRadius: 0

            },
            {
                label: 'CEX GBP',
                //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
                data: count_cex_gbp_to_zar,
                backgroundColor: 'rgba(241, 244, 65, 0.1)',
                borderColor: 'rgba(241, 244, 65, 1)',
                borderWidth: 1,
                pointRadius: 0

            }


            ]
        };
        chart_all_to_zar.data = data_luno_zar;
        chart_all_to_zar.update();

    });
}