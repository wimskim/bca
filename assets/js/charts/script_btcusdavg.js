var myMovingAverage = function (arr, win) {
  var filtered = medianFilter(arr, 5);
  var prefill = new Array(win);
  prefill.fill(0);
  filtered = prefill.concat(filtered);
  averaged = movingAverage(filtered, win);
  return averaged.slice(win);
}

var ctx_count_btcusd = document.getElementById('chart_btcusd').getContext('2d');


// global settings:

Chart.defaults.global.animation = false;
Chart.defaults.global.elements.line.tension = 0;

var chart_btcusd = new Chart(ctx_count_btcusd, {
  type: 'line',
  options: {
    scales: {
      yAxes: [{
        ticks: {
          callback: function (value, index, values) { return value.toPrecision(0) + ' USD'; }
        }
      }]
    },
    tooltips: {
      callbacks: {
        label: function (tooltipItem, data) { return tooltipItem.yLabel.toFixed(0) + ' USD'; }
      }
    }
  }
});


var drawCharts = function (days) {

  fetch('json.aspx?getjson=getbtcusdavg&days=' + days).then(function (response) {
    return response.json();
  }).then(function (blockData) {
    blockData.BitlishUSD = blockData.BitlishUSD.sort(function (a, b) { return a.id - b.id; });
  

    var periods = blockData.BitlishUSD.length;

    //if (periods > blockData.BitlishUSD.length) {
    //    periods = 0;
    //}



    var count_BitlishUSD = blockData.BitlishUSD.map(function (item) { return item.p; });
 

    var data_count_percent = {
      labels: blockData.BitlishUSD.map(function (item) { return item.ts; }).slice(-periods),
      datasets: [{
        label: 'Bitlish USD',
        //data: myMovingAverage(count_BitlishUSD, 4).slice(-periods),
        data: count_BitlishUSD,
        backgroundColor: 'rgba(71, 65, 244, 0.1)',
        borderColor: 'rgba(71, 65, 244, 1)',
        borderWidth: 1,
        pointRadius: 0

      }
      ]
    };

    chart_prof_percent.data = data_count_percent;
    chart_prof_percent.update();

    var count_luno_zar = blockData.BitlishUSD.map(function (item) { return item.bid; });
    var count_bitlish_usd_to_zar = blockData.BitlishUSD.map(function (item) {
      var price = (item.ask * (item.rate + 0.4));
      var fees = 3.5 + 0.2;
      price = price + (price * (fees / 100));
      price = price + (0.001 * item.ask); // transfer fee
      return price;
    });
    var count_bitlish_eur_to_zar = blockData.BitlishEUR.map(function (item) {
      var price = (item.ask * (item.rate + 0.4));
      var fees = 3.5 + 0.2;
      price = price + (price * (fees / 100));
      price = price + (0.001 * item.ask); // transfer fee
      return price;
    });
    var count_cex_usd_to_zar = blockData.CEXUSD.map(function (item) {
      var price = (item.ask * (item.rate + 0.4));
      var fees = 3.5 + 0.2;
      price = price + (price * (fees / 100));
      price = price + (0.001 * item.ask); // transfer fee
      return price;
    });
    var count_cex_eur_to_zar = blockData.CEXEUR.map(function (item) {
      var price = (item.ask * (item.rate + 0.4));
      var fees = 3.5 + 0.2;
      price = price + (price * (fees / 100));
      price = price + (0.001 * item.ask); // transfer fee
      return price;
    });
    var count_cex_gbp_to_zar = blockData.CEXGBP.map(function (item) {
      var price = (item.ask * (item.rate + 0.4));
      var fees = 3.5 + 0.2;
      price = price + (price * (fees / 100));
      price = price + (0.001 * item.ask); // transfer fee
      return price;
    });
    var count_bitfinex_usd_to_zar = blockData.BitFinexUSD.map(function (item) {
      var price = (item.ask * (item.rate + 0.4));
      var fees = 3.5 + 0.2;
      price = price + (price * (fees / 100));
      price = price + (0.001 * item.ask); // transfer fee
      return price;
    });

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
        pointRadius: 0,
        hidden: true,

      }


      ]
    };
    chart_all_to_zar.data = data_luno_zar;
    chart_all_to_zar.update();

    var count_usdzar = blockData.BitlishUSD.map(function (item) { return item.rate; });
    var count_eurzar = blockData.BitlishEUR.map(function (item) { return item.rate; });
    var count_gbpzar = blockData.CEXGBP.map(function (item) { return item.rate; });
    var data_exchangerates = {
      labels: blockData.BitlishUSD.map(function (item) { return item.ts; }).slice(-periods),
      datasets: [{
        label: 'USD',
        //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
        data: count_usdzar,
        backgroundColor: 'rgba(71, 65, 244, 0.1)',
        borderColor: 'rgba(71, 65, 244, 1)',
        borderWidth: 1,
        pointRadius: 0

      },
      {
        label: 'EUR',
        hidden: true,
        //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
        data: count_eurzar,
        backgroundColor: 'rgba(32, 158, 28, 0.1)',
        borderColor: 'rgba(32, 158, 28, 1)',
        borderWidth: 1,
        pointRadius: 0

      },
      {
        label: 'GBP',
        hidden: true,
        //data: myMovingAverage(count_BitlishUSD_Price, 4).slice(-periods),
        data: count_gbpzar,
        backgroundColor: 'rgba(241, 244, 65, 0.1)',
        borderColor: 'rgba(241, 244, 65, 1)',
        borderWidth: 1,
        pointRadius: 0

      },
      ]
    };
    chart_all_exchangerates.data = data_exchangerates;
    chart_all_exchangerates.update();


  

   
  });


  


}
  


drawCharts(1);


// reload the page every 5 minutes
setTimeout(RefreshPage, (1000 * 60 * 5))
function RefreshPage() {
  location.reload();
}