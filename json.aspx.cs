using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CryptoTrader
{
    public partial class json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // this is for ajax json requests
            if (Request.QueryString["getjson"] != null)
            {

                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.MinValue);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetValidUntilExpires(false);

                Response.Expires = -1;
                Response.ExpiresAbsolute = DateTime.MinValue;
                Response.AddHeader("Cache-Control", "no-cache");
                Response.AddHeader("Pragma", "no-cache");


                if (Request.QueryString["getjson"] == "getprofits")
                {

                    Response.Write(GetProfitJSON(int.Parse(Request["days"]), Request["arbsymbol"]));
                    Response.End();
                }
              

            }
        }
        private string GetProfitJSON(int days,string arbSymbol)
        {
            DateTime dtto = DateTime.Now;
            DateTime dtfrom = DateTime.Now.AddDays(days * -1);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

          

            sb.Append("{");

            sb.Append("\"BitlishUSD\": [");
            BLL.ProfitRecordingCollection profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.USD, dtfrom,dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            sb.Append("],");
            sb.Append("\"BitlishEUR\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Bitlish, BLL.ProfitRecording.enCurrency.EUR,dtfrom, dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            
            sb.Append("],");
            sb.Append("\"BitstampUSD\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Bitstamp, BLL.ProfitRecording.enCurrency.USD, dtfrom, dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            sb.Append("],");
            sb.Append("\"BitstampEUR\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Bitstamp, BLL.ProfitRecording.enCurrency.EUR, dtfrom, dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }

            sb.Append("],");
            sb.Append("\"CEXUSD\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.USD, dtfrom, dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            sb.Append("],");
            sb.Append("\"CEXEUR\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.EUR, dtfrom,dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            sb.Append("],");
            sb.Append("\"CEXGBP\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Cex, BLL.ProfitRecording.enCurrency.GBP, dtfrom,dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            sb.Append("],");
            sb.Append("\"BitFinexUSD\": [");
            profits = BLL.ProfitRecordingCollection.GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange.Bitfinex, BLL.ProfitRecording.enCurrency.USD, dtfrom, dtto, days, arbSymbol);
            for (int x = 0; x < profits.Count; x++)
            {
                BLL.ProfitRecording pf = profits[x];
                sb.Append("{");
                sb.Append("\"id\":" + pf.Id.ToString() + ",");
                sb.Append("\"ts\":\"" + pf.TimeStamp.ToString("d MMM yyyy, HH:mm") + "\",");
                sb.Append("\"p\":" + pf.ProfitPerc + ",");
                sb.Append("\"rate\":" + pf.CurrencyToZARExchangeRate + ",");
                sb.Append("\"ask\":" + pf.ExchangeAsk + ",");
                sb.Append("\"bid\":" + pf.LunoBid + "");
                sb.Append("}");

                if (x < profits.Count - 1)
                    sb.Append(",");

            }
            sb.Append("]");


            sb.Append("}");
            return sb.ToString();
        }
    }
}