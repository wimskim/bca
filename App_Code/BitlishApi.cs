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
    public static class BitlishApi
    {

        public static BestBitlishBidAsk GetBestPrices(BitlsihSymbolEnum pair, decimal minVol)
        {

            var client = new RestClient("https://bitlish.com/api/v1/trades_depth");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{ \"pair_id\": \"" + pair.ToString().ToLower() + "\" }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            BitlishResponse bitlishResponse;
            try
            {
                bitlishResponse = JsonConvert.DeserializeObject<BitlishResponse>(response.Content);

                BestBitlishBidAsk bestprices = new BestBitlishBidAsk();
               
                int i = 0;
                do
                {
                    bestprices.ask.price = bitlishResponse.ask[i].price;
                    bestprices.ask.volume += bitlishResponse.ask[i].volume;
                    i++;
                } while (bestprices.ask.volume < minVol);

                i = 0;
                do {
                    bestprices.bid.price = bitlishResponse.bid[i].price;
                    bestprices.bid.volume += bitlishResponse.bid[i].volume;
                    i++;
                }
                while (bestprices.bid.volume  < minVol);

                    return bestprices;
            }
            catch
            {
                return new BestBitlishBidAsk();
            }


        }
    
    }

    public class BitlishResponse
    {
        public List<BitlishItem> ask { get; set; }
        public List<BitlishItem> bid { get; set; }
    }

    public class BitlishItem
    {
        public decimal price { get; set; }
        public decimal volume { get; set; }
    }

    public class BestBitlishBidAsk
    {
        public BestBitlishBidAsk()
        {
            bid = new BitlishItem();
            ask = new BitlishItem();
        }
        public BitlishItem bid { get; set; }
        public BitlishItem ask { get; set; }
    }

    public enum BitlsihSymbolEnum
    {
        BtcUsd = 1,
        BtcEur = 2,
        BtcRub = 3,
        BtcGbp = 4,
        EthBtc = 5,
        EthUsd = 6,
        EthEur = 7,
    }

}
