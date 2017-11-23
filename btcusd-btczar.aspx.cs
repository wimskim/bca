using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CryptoTrader
{
    public partial class btcusd_btczar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BindPage();

        }
        private void BindPage()
        {

         
            lblProfits.InnerHtml += "<br>";

            string strcolor = "green";
            decimal usdzar = 0;
            decimal eurzar = 0;
            decimal gbpzar = 0;

            decimal cexusd = 0;
            decimal bitfinexusd = 0;

            decimal biggestProfit = -100;


            System.Collections.Generic.List<BLL.ProfitRecording> pfs = new List<BLL.ProfitRecording>();


            BLL.ProfitRecording pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.USD,"BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" +strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            usdzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.EUR, "BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            eurzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;

            lblProfits.InnerHtml += "<br>";



            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitstamp, BLL.ProfitRecording.enCurrency.USD, "BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitstamp USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            usdzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitstamp, BLL.ProfitRecording.enCurrency.EUR, "BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitstamp EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            eurzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;

            lblProfits.InnerHtml += "<br>";



            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.USD, "BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            usdzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;
            cexusd = pf.ExchangeAsk;


            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.EUR, "BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.GBP, "BTC");
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX GBP: &pound;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            gbpzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);
            if (pf.ProfitPerc > biggestProfit) biggestProfit = pf.ProfitPerc;

            lblProfits.InnerHtml += "<br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitfinex, BLL.ProfitRecording.enCurrency.USD, "BTC");
          //  strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
           // lblProfits.InnerHtml += "Bitfinex USD: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            pfs.Add(pf);
            bitfinexusd = pf.ExchangeAsk;

            BLL.ProfitRecording latest = pfs.Find(delegate (BLL.ProfitRecording p) { return p.TimeStamp == pfs.Max(t => t.TimeStamp); });


           // lblProfits.InnerHtml += "<br>";
            lblProfits.InnerHtml += "Luno ZAR: R" + latest.LunoBid.ToString("0") + "<br>";
            lblProfits.InnerHtml += "<br>";
            lblProfits.InnerHtml += "USDZAR: R" + usdzar.ToString("0.00")  + "<br>";
            lblProfits.InnerHtml += "EURZAR: R" + eurzar.ToString("0.00") + "<br>";
            lblProfits.InnerHtml += "GBPZAR: R" + gbpzar.ToString("0.00") + "<br>";



            lblCEXBitfinex.InnerHtml += "<br>CEX:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$" + cexusd.ToString("0");
            lblCEXBitfinex.InnerHtml += "<br>Bitfinex:&nbsp;&nbsp;$" + bitfinexusd.ToString("0");
            lblCEXBitfinex.InnerHtml += "<br>Perc difference:  " + (((cexusd / bitfinexusd) * 100) - 100).ToString("0.00") + "%";



            ((CryptoTrader.Template.Site)this.Page.Master).PageTitle = "Crypto Trades " + biggestProfit.ToString("0.00") + "%";



        }



    }
}