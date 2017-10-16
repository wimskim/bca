using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoTrader
{
    public static class BitfinexApi
    {

        public static BestBitfinexBidAsk GetBestPrices(BitfinexSymbolEnum pair, decimal minVol)
        {

            var client = new RestClient("https://api.bitfinex.com/v1/book/" + pair.ToString().ToUpper());
            var request = new RestRequest(Method.GET);
            
            IRestResponse response = client.Execute(request);
            BitfinexResponse BitfinexResponse;
            try
            {
                BitfinexResponse = JsonConvert.DeserializeObject<BitfinexResponse>(response.Content);

                BestBitfinexBidAsk bestprices = new BestBitfinexBidAsk();

                int i = 0;
                do
                {
                    bestprices.ask.price = BitfinexResponse.asks[i].price;
                    bestprices.ask.amount += BitfinexResponse.asks[i].amount;
                    i++;
                } while (bestprices.ask.amount < minVol);

                i = 0;
                do
                {
                    bestprices.bid.price = BitfinexResponse.bids[i].price;
                    bestprices.bid.amount += BitfinexResponse.bids[i].amount;
                    i++;
                }
                while (bestprices.bid.amount < minVol);

                return bestprices;
            }
            catch
            {
                return new BestBitfinexBidAsk();
            }


        }

    }

    public class BitfinexResponse
    {
        public List<BitfinexItem> asks { get; set; }
        public List<BitfinexItem> bids { get; set; }
    }

    public class BitfinexItem
    {
        public decimal price { get; set; }
        public decimal amount { get; set; }
    }

    public class BestBitfinexBidAsk
    {
        public BestBitfinexBidAsk()
        {
            bid = new BitfinexItem();
            ask = new BitfinexItem();
        }
        public BitfinexItem bid { get; set; }
        public BitfinexItem ask { get; set; }
    }

    public enum BitfinexSymbolEnum
    {
        BtcUsd = 1,
        
    }

}
