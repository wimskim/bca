using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CryptoTrader;
using System.Globalization;

namespace CryptoTrader
{
    public partial class ethbtc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindPage();
        }

        private void BindPage()
        {

            var bitlish = BitlishApi.GetBestPrices(BitlsihSymbolEnum.EthBtc, decimal.Parse(txtEth.Value, CultureInfo.InvariantCulture));

            lblBitlishAsk.InnerText = bitlish.ask.price.ToString("0.00000000");
            lblBitlishBid.InnerText = bitlish.bid.price.ToString("0.00000000");

            var cex = CexApi.GetBestPrices(CEXSymbolEnum.Eth_Btc, decimal.Parse(txtEth.Value, CultureInfo.InvariantCulture));

            lblCexAsk.InnerText = cex.ask.price.ToString("0.00000000");
            lblCexBid.InnerText = cex.bid.price.ToString("0.00000000");


            decimal avgAsk = (bitlish.ask.price + cex.ask.price) / 2;
            decimal avgBid = (bitlish.bid.price + cex.bid.price) / 2;
            decimal avgPrice = (avgBid + avgAsk) / 2;
            decimal avgGap = (avgAsk - avgBid);
            avgGap = (avgGap / avgPrice) * 100;
            lblAvgGap.InnerHtml = avgGap.ToString("0.00000000");



            decimal decEth = decimal.Parse(txtEth.Value, CultureInfo.InvariantCulture);

            // Bitlish to CEX
            //decimal decBtcSold = bitlish.bid.price * decEth;
            //decimal decBtcBought = decEth * cex.ask.price;
            //decimal decBtcProfit = decBtcBought - decBtcSold;
            //decimal decBtcProfitPerc = (decBtcProfit / decBtcBought) * 100;
            //divBtoC_Eth.InnerHtml = decEth.ToString("0.00000000");
            //divBtoC_BTCsold.InnerHtml = decBtcSold.ToString("0.00000000") + " @ " + bitlish.bid.price.ToString("0.00000000");
            //divBtoC_BtcBought.InnerHtml = decBtcBought.ToString("0.00000000") + " @ " + cex.ask.price.ToString("0.00000000"); ;
            //divBtoC_ProfitBTC.InnerHtml = decBtcProfit.ToString("0.00000000");
            //divBtoC_ProfitPerc.InnerHtml = decBtcProfitPerc.ToString("0.00000000");

            decimal decTakerFee1 = 0;

            decimal decBtcSold = bitlish.ask.price * decEth;
            decTakerFee1 = (decEth * (0.2m / 100));
            decimal decEthMinusFees = decEth - decTakerFee1;
            
            decimal decBtcBought = decEthMinusFees * cex.bid.price;
            decimal decTakerFee2 = (decBtcBought * (0.2m / 100));
            decimal decBtcBoughtMinusFees = decBtcBought - decTakerFee2;
            decimal decBtcProfit = decBtcBoughtMinusFees - decBtcSold;
            decimal decBtcProfitPerc = (decBtcProfit / decBtcBought) * 100;

            divBtoC_Eth.InnerHtml = decEth.ToString("0.00000000") + " ETH<BR> - " + decTakerFee1.ToString("0.00000000") + " ETH<BR> = " + decEthMinusFees.ToString("0.00000000") + " ETH";
            divBtoC_BTCsold.InnerHtml = decBtcSold.ToString("0.00000000") + " @ " + bitlish.ask.price.ToString("0.00000000");
            divBtoC_BtcBought.InnerHtml = decBtcBought.ToString("0.00000000") + " BTC @ " + cex.bid.price.ToString("0.00000000") + " <BR> - " + decTakerFee2.ToString("0.00000000") + " BTC<BR> = " + decBtcBoughtMinusFees.ToString("0.00000000") + " BTC";
            divBtoC_ProfitBTC.InnerHtml = decBtcProfit.ToString("0.00000000");
            divBtoC_ProfitPerc.InnerHtml = decBtcProfitPerc.ToString("0.00000000");

            // CEX to Bitlis
            //decBtcSold = cex.bid.price * decEth;
            //decBtcBought = decEth * bitlish.ask.price;
            //decBtcProfit = decBtcBought - decBtcSold;
            //decBtcProfitPerc = (decBtcProfit / decBtcBought) * 100;
            //divCtoB_Eth.InnerHtml = decEth.ToString("0.00000000");
            //divCtoB_BTCsold.InnerHtml = decBtcSold.ToString("0.00000000") + " @ " + cex.bid.price.ToString("0.00000000");
            //divCtoB_BtcBought.InnerHtml = decBtcBought.ToString("0.00000000") + " @ " + bitlish.ask.price.ToString("0.00000000"); ;
            //divCtoB_ProfitBTC.InnerHtml = decBtcProfit.ToString("0.00000000");
            //divCtoB_ProfitPerc.InnerHtml = decBtcProfitPerc.ToString("0.00000000");


            decBtcSold = cex.ask.price * decEth;
            decTakerFee1 = (decEth * (0.2m / 100));
            decEthMinusFees = decEth - decTakerFee1;

            decBtcBought = decEthMinusFees * bitlish.bid.price;
            decTakerFee2 = (decBtcBought * (0.2m / 100));
            decBtcBoughtMinusFees = decBtcBought - decTakerFee2;
            decBtcProfit = decBtcBoughtMinusFees - decBtcSold;
            decBtcProfitPerc = (decBtcProfit / decBtcBought) * 100;

            divCtoB_Eth.InnerHtml = decEth.ToString("0.00000000") + " ETH<BR> - " + decTakerFee1.ToString("0.00000000") + " ETH<BR> = " + decEthMinusFees.ToString("0.00000000") + " ETH";
            divCtoB_BTCsold.InnerHtml = decBtcSold.ToString("0.00000000") + " @ " + cex.ask.price.ToString("0.00000000");
            divCtoB_BtcBought.InnerHtml = decBtcBought.ToString("0.00000000") + " BTC @ " + bitlish.bid.price.ToString("0.00000000") + " <BR> - " + decTakerFee2.ToString("0.00000000") + " BTC<BR> = " + decBtcBoughtMinusFees.ToString("0.00000000") + " BTC";
            divCtoB_ProfitBTC.InnerHtml = decBtcProfit.ToString("0.00000000");
            divCtoB_ProfitPerc.InnerHtml = decBtcProfitPerc.ToString("0.00000000");

        }

        protected void lnkSave1_Click(object sender, EventArgs e)
        {
            BindPage();
        }
    }
}