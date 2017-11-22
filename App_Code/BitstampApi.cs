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
    public static class BitstampApi
    {
        public const int MinVolume = 1;
        public static BestBitstampBidAsk GetBestPrices(BitstampSymbolEnum pair, decimal minVol)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            var client = new RestClient("https://www.bitstamp.net/api/v2/order_book/" + pair.ToString().ToUpper());

            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            
            BitstampResponse BitstampResponse;
            BitstampResponse = JsonConvert.DeserializeObject<BitstampResponse>(response.Content);

            try
            {

                BestBitstampBidAsk bestprices = new BestBitstampBidAsk();

                int i = 0;
                do
                {
                    bestprices.ask.price = BitstampResponse.asks[i][0];
                    bestprices.ask.volume += BitstampResponse.asks[i][1];
                    i++;
                } while (bestprices.ask.volume < minVol);

                i = 0;
                do
                {
                    bestprices.bid.price = BitstampResponse.bids[i][0];
                    bestprices.bid.volume += BitstampResponse.bids[i][1];
                    i++;
                }
                while (bestprices.bid.volume < minVol);

                return bestprices;
            }
            catch
            {
                return new BestBitstampBidAsk();
            }
        }
    }

    public class BitstampResponse
    {
        public List<List<decimal>> bids { get; set; }
        public List<List<decimal>> asks { get; set; }
    }

    public class BitstampItem
    {
        public decimal price { get; set; }
        public decimal volume { get; set; }
    }
    public class BestBitstampBidAsk
    {
        public BestBitstampBidAsk()
        {
            bid = new BitstampItem();
            ask = new BitstampItem();
        }
        public BitstampItem bid { get; set; }
        public BitstampItem ask { get; set; }
    }
    public enum BitstampSymbolEnum
    {
        BTCUSD = 1,
        BTCEUR = 2
    }

}
