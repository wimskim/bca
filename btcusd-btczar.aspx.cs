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

            BLL.ProfitRecording pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.USD);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" +strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font><br>";
            decimal usdzar = pf.CurrencyToZARExchangeRate;

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.EUR);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font><br>";
            decimal eurzar = pf.CurrencyToZARExchangeRate;

            lblProfits.InnerHtml += "<br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.USD);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font><br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.EUR);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font><br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.GBP);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX GBP: &pound;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font><br>";
            decimal gbpzar = pf.CurrencyToZARExchangeRate;

            lblProfits.InnerHtml += "<br>";
            lblProfits.InnerHtml += "Luno ZAR: R" + pf.LunoBid.ToString("0") + "<br>";
            lblProfits.InnerHtml += "USDZAR: R" + usdzar.ToString("0.00")  + "<br>";
            lblProfits.InnerHtml += "EURZAR: R" + eurzar.ToString("0.00") + "<br>";
            lblProfits.InnerHtml += "GBPZAR: R" + gbpzar.ToString("0.00") + "<br>";

            lblProfits.InnerHtml += "<br>";

            lblProfits.InnerHtml += "Last Update: " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";

        }

   

    }
}