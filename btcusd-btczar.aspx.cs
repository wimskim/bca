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


            //BLL.ProfitRecording.GenerateProfitRecordings();

            lblProfits.InnerHtml += "<br>";

            string strcolor = "green";

            BLL.ProfitRecording pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.USD);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish USD: <font color=\"" +strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font>  @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";
            
            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.EUR);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "Bitlish EUR: <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font>  @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.USD);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX USD: <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font>  @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.EUR);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX EUR: <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font>  @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";

            pf = BLL.ProfitRecording.GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.GBP);
            strcolor = (pf.ProfitPerc < 0 ? "red" : "green");
            lblProfits.InnerHtml += "CEX GBP: <font color=\"" + strcolor + "\">" + pf.ProfitPerc.ToString("0.00") + "%</font>  @ " + pf.TimeStamp.ToString("d MMM yyyy, HH:mm:ss") + "<br>";
        }

   

    }
}