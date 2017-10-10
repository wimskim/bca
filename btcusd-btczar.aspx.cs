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

            System.Collections.Generic.List<BLL.ProfitRecording> pfs = new List<BLL.ProfitRecording>();


            BLL.ProfitRecording pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.USD);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" +strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            usdzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.EUR);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            eurzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);

            lblProfits.InnerHtml += "<br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.USD);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX USD: $" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            usdzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);


            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.EUR);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX EUR: &euro;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            pfs.Add(pf);
            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.GBP);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX GBP: &pound;" + pf.ExchangeAsk.ToString("0") + ":  <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font> @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + " <br>";
            gbpzar = pf.CurrencyToZARExchangeRate;
            pfs.Add(pf);

            BLL.ProfitRecording latest = pfs.Find(delegate (BLL.ProfitRecording p) { return p.TimeStamp == pfs.Max(t => t.TimeStamp); });


            lblProfits.InnerHtml += "<br>";
            lblProfits.InnerHtml += "Luno ZAR: R" + latest.LunoBid.ToString("0") + "<br>";
            lblProfits.InnerHtml += "USDZAR: R" + usdzar.ToString("0.00")  + "<br>";
            lblProfits.InnerHtml += "EURZAR: R" + eurzar.ToString("0.00") + "<br>";
            lblProfits.InnerHtml += "GBPZAR: R" + gbpzar.ToString("0.00") + "<br>";

            lblProfits.InnerHtml += "<br>";

            lblProfits.InnerHtml += "Last Update: " + latest.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";

        }

   

    }
}