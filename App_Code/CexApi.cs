using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace CryptoTrader
{
    public static class CexApi
    {
        public const int MinVolume = 1;
        public static BestCEXBidAsk GetBestPrices(CEXSymbolEnum pair, decimal minVol)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            var client = new RestClient("https://cex.io/api/order_book/" + pair.ToString().ToUpper().Replace("_", "/")+ "/?depth=50");
            
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            


            CexResponse cexResponse;
            cexResponse = JsonConvert.DeserializeObject<CexResponse>(response.Content);

            try
            {
            
                BestCEXBidAsk bestprices = new BestCEXBidAsk();

                int i = 0;
                do
                {
                    bestprices.ask.price = cexResponse.asks[i][0];
                    bestprices.ask.volume += cexResponse.asks[i][1];
                    i++;
                } while (bestprices.ask.volume < minVol);

                i = 0;
                do
                {
                    bestprices.bid.price = cexResponse.bids[i][0];
                    bestprices.bid.volume += cexResponse.bids[i][1];
                    i++;
                }
                while (bestprices.bid.volume < minVol);

                return bestprices;
            }
            catch
            {
                return new BestCEXBidAsk();
            }
        }
    }

    public class CexResponse
    {
        public List<List<decimal>> bids { get; set; }
        public List<List<decimal>> asks { get; set; }
    }

    public class CexItem
    {
        public decimal price { get; set; }  
        public decimal volume { get; set; }
    }
    public class BestCEXBidAsk
    {
        public BestCEXBidAsk()
        {
            bid = new CexItem();
            ask = new CexItem();
        }
        public CexItem bid { get; set; }
        public CexItem ask { get; set; }
    }
    public enum CEXSymbolEnum
    {
        Btc_Usd = 1,
        Btc_Eur = 2,
        Btc_Rub = 3,
        Btc_Gbp = 4,
        Eth_Btc = 5,
        Eth_Usd = 6,
        Eth_Eur = 7,
        Eth_Gbp = 8
    }

}
